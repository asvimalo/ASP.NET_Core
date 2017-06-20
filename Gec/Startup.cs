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

namespace Gec
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env, IConfigurationRoot config)
        {
            _env = env;
            _config = config;
            //To be able to load the json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json");
            //Set the configuration
            _config = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            if (_env.IsDevelopment() || _env.IsEnvironment("Testing"))
            {
                services.AddScoped<ImailService, DebugMailService>(); 
            }
            else
            {
                // implement the real one
            }
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env/*, ILoggerFactory loggerFactory*/)
        {   //1.
            //app.UseDefaultFiles(); => once a controller 
            //has control over a view the default index won t be used
            if (env.IsEnvironment("Development"))
                app.UseDeveloperExceptionPage();


            app.UseStaticFiles();

            //2. Use Mvc to activate the comunication between controller and view
            //Use MapRoute to listen to requests
            

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Gec", action = "Index" }
                );
            });

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
