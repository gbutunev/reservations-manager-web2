using project2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace project2.Repositories
{
    public class UsersRepository
    {

        private IzrezoDbContext context = null;

        public UsersRepository()
        {
            context = new IzrezoDbContext();
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public void Insert(User u)
        {
            context.Users.Add(u);
            context.SaveChanges();
        }

        public User GetFirstOrDefault(Expression<Func<User, bool>> filter)
        {
            return context.Users.Where(filter).FirstOrDefault();
        }

        public User GetById(int id)
        {
            return context.Users.Where(i => i.Id == id).FirstOrDefault();
        }

        public void Delete(int id)
        {
            User u = GetById(id);

            if (u != null)
            {
                context.Users.Remove(u);
                context.SaveChanges();
            }
        }

        public void Update(User u)
        {
            DbEntityEntry entry = context.Entry(u);
            entry.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}