using System;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System.Linq;

#nullable disable

namespace DB
{
    public class MySQLApplicationContext: DbContext
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

        public MySQLApplicationContext(string conn)
        {
            ConnectionString = conn;
        }

        public MySQLApplicationContext(DbContextOptions<ApplicationContext> options):
                base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 29)));
            }
		}
    }
}