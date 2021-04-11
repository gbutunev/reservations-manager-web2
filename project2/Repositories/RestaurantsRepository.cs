using project2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project2.Repositories
{
    public class RestaurantsRepository
    {
        //no update option
        //no getFirstOrDefault

        private IzrezoDbContext context = null;

        public RestaurantsRepository()
        {
            context = new IzrezoDbContext();
        }

        public List<Restaurant> GetAll()
        {
            return context.Restaurants.ToList();
        }

        public void Insert(Restaurant r)
        {
            context.Restaurants.Add(r);
            context.SaveChanges();
        }

        public Restaurant GetById(int id)
        {
            return context.Restaurants.Where(i => i.Id == id).FirstOrDefault();
        }

        public Restaurant GetByName(string name)
        {
            return context.Restaurants.Where(i => i.Name.Equals(name)).FirstOrDefault();
        }

        public void Delete(int id)
        {
            Restaurant r = GetById(id);

            if (r != null)
            {
                context.Restaurants.Remove(r);
                context.SaveChanges();
            }
        }
    }
}