using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Gec.Services;
using Microsoft.Extensions.Configuration;
using Gec.Models;
using Gec.EF;
using Gec.EF.Db;
using Gec.EF.IRepo;
using Gec.EF.Repo;
using Gec.Models.Gec;
using Gec.Models.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gec
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            //To be able to load the json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            //Set the configuration
            _config = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        // Go ahead and let the task complete
                        await Task.Yield();
                    },
                    OnRedirectToAccessDenied = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 403;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        // Go ahead and let the task complete
                        await Task.Yield();
                    }
                };
            })
            .AddEntityFrameworkStores<GecContext>();
            //services.AddEntityFramework()
            //    .AddEntityFrameworkSqlServer()
            //    .AddDbContext<GecContext>();

            #region IdentityOptions (conf)
            ///Second Option to configure Identity
            //services.Configure<IdentityOptions>(conf =>
            //{
            //    conf.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
            //    {
            //        OnRedirectToLogin = (ctx) =>
            //        {
            //            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
            //            {
            //                ctx.Response.StatusCode = 401;
            //            }
            //            return Task.CompletedTask;
            //        },
            //        OnRedirectToAccessDenied = (ctx) =>
            //        {
            //            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
            //            {
            //                ctx.Response.StatusCode = 401;
            //            }
            //            return Task.CompletedTask;
            //        }
            //    };
            //}); 
            #endregion


            services.AddSingleton(_config);

            if (_env.IsDevelopment() || _env.IsEnvironment("Testing"))
            {
                services.AddScoped<IEmailService, DebugMailService>();
            }
            else
            {
                // implement the real one
            }
            services.AddDbContext<GecContext>();
            services.AddScoped<IFeedRepo, FeedRepo>();
            services.AddScoped<ICommentRepo, CommentRepo>();
            services.AddScoped<IPictureRepo, PictureRepo>();
            services.AddScoped<IPlaygroundRepo, PlaygroundRepo>();
            services.AddScoped<IAccountRepo, AccountRepo>();

            services.AddTransient<GeoCoordsService>();
            services.AddTransient<GecContextSeedData>();

            services.AddLogging();

            services.AddAuthorization(conf =>
            {
                conf.AddPolicy("SuperUsers", p => p.RequireClaim("SuperUser", "True"));
            });

            services.AddMvc(config =>
            {
                if (!_env.IsEnvironment("IsProduction"))
                {
                    config.SslPort = 44388;

                }
                config.Filters.Add(new RequireHttpsAttribute());
            })
            .AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // It was already camelcasing before this config
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            GecContextSeedData seeder,
            ILoggerFactory loggerFactory)
        {

            // 1.
            // app.UseDefaultFiles(); => once a controller 
            // has control over a view the default index won t be used

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
                loggerFactory.AddDebug(LogLevel.Error);



            app.UseStaticFiles();
            app.UseIdentity();

            //2. Use Mvc to activate the comunication between controller and view
            //Use MapRoute to listen to requests
            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = _config["Token:Issuer"],
                    ValidAudience = _config["Token:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"])),
                    ValidateLifetime = true
                }


            });

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Gec", action = "Index" }
                );
            });
            seeder.EnsureSeedData().Wait();

            #region Default
            //loggerFactory.AddConsole();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            #endregion

        }
    }
}
