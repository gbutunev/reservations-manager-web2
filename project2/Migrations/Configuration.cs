namespace project2.Migrations
{
    using project2.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<project2.Repositories.IzrezoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(project2.Repositories.IzrezoDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            if (context.Cities.Count() <= 0)
            {
                City c = new City
                {
                    Id = 1,
                    Name = "Plovdiv"
                };

                context.Cities.Add(c);
                context.SaveChanges();
            }

            if (context.Users.Count() <= 0)
            {
                User u = new User()
                {
                    Username = "admin",
                    Password = "admin",
                    FirstName = "Georgi",
                    LastName = "Adminov",
                    isAdmin = true,
                    //LocationId = 1

                };

                context.Users.Add(u);
                context.SaveChanges();
            }

        }
    }
}
