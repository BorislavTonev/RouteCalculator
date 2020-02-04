using Microsoft.EntityFrameworkCore;
using Router.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RouteCalculator.Data
{
    public class RouterContext: DbContext
    {
        private string _conectionString;
        public RouterContext(DbContextOptions<RouterContext> options)
            :base(options)
        {
        }

        public RouterContext(string conectionString)
        {
            _conectionString = conectionString;
        }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Road> Roads { get; set; }
        public DbSet<LogisticsCenter> LogisticsCenters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=RouteCalculator;Trusted_Connection=True;");
            //"Data Source=.;Initial Catalog=RouteCalculator;Trusted_Connection=True;"

        }
    }
}
