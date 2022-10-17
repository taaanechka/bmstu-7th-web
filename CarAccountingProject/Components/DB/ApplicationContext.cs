using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

#nullable disable

namespace DB
{
    public class ApplicationContext: DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Coming> Comings { get; set; }
        public virtual DbSet<Departure> Departures { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarOwner> CarOwners { get; set; }
        public virtual DbSet<LinkOwnerCarDeparture> LinksOwnerCarDeparture { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<VIPCarOwner> VIPCarOwners { get; set; }


        private string ConnectionString { get; set; }
        //public string ConnectionString { get; set; }

        public ApplicationContext(string conn)
        {
            ConnectionString = conn;
        }

        // public ApplicationContext(ApplicationContext oldContext)
        // {
        //     ConnectionString = oldContext.ConnectionString;
        // }

        public ApplicationContext(DbContextOptions<ApplicationContext> options):
                base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConnectionString);
            }
		}
    }
}