using project2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project2.Repositories
{
    public class CitiesRepository
    {

        //need to add update class
        //no GetFirstOrDefault C:\Users\georg\source\repos\WP2project\WP2project\Views\Client\AllReservations.cshtml

        private IzrezoDbContext context = null;

        public CitiesRepository()
        {
            context = new IzrezoDbContext();
        }

        public List<City> GetAll()
        {
            return context.Cities.ToList();
        }

        public void Insert(City c)
        {
            context.Cities.Add(c);
            context.SaveChanges();
        }

        public City GetById(int id)
        {
            return context.Cities.Where(i => i.Id == id).FirstOrDefault();
        }

        public City GetByName(string name)
        {
            return context.Cities.Where(i => i.Name.Equals(name)).FirstOrDefault();
        }

        public void Delete(int id)
        {
            City c = GetById(id);

            if (c != null)
            {
                context.Cities.Remove(c);
                context.SaveChanges();
            }
        }
    }
}