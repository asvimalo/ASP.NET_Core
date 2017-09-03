using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gec.Models.Playground;
using Microsoft.Extensions.Configuration;
using Gec.Models.Gec;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Gec.Models.Account;

namespace Gec.EF.Db
{
    public class GecContext : IdentityDbContext<User>
    {
        private IConfigurationRoot _config;

        public GecContext(IConfigurationRoot config, DbContextOptions options):base(options)
        {
            _config = config;
        }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:GecContextConnection"]);
        }
    }
}
