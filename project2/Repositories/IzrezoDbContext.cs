using project2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project2.Repositories
{
    public class IzrezoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public IzrezoDbContext() : base(@"Server=serveriphostname;Database=Reservations2;User Id=sa;Password=pwd;")
        {
            Users = this.Set<User>();
            Restaurants = this.Set<Restaurant>();
            Cities = this.Set<City>();
            Reservations = this.Set<Reservation>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<IzrezoDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}