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
    public class ReservationsRepository
    {
        private IzrezoDbContext context = null;

        public ReservationsRepository()
        {
            context = new IzrezoDbContext();
        }

        public List<Reservation> GetAll(Expression<Func<Reservation, bool>> filter)
        {
            IQueryable<Reservation> query = context.Reservations;

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public List<Reservation> GetAllReservations()
        {
            return context.Reservations.ToList();
        }

        public void Insert(Reservation res)
        {
            context.Reservations.Add(res);

            context.SaveChanges();
        }

        public void Update(Reservation res)
        {
            DbEntityEntry entry = context.Entry(res);
            entry.State = EntityState.Modified;

            context.SaveChanges();
        }

        public Reservation GetById(int id)
        {
            return context.Reservations
                            .Where(i => i.Id == id)
                            .FirstOrDefault();
        }

        public Reservation GetFirstOrDefault(Expression<Func<Reservation, bool>> filter)
        {
            return context.Reservations
                      .Where(filter)
                      .FirstOrDefault();
        }

        public void Delete(int id)
        {
            Reservation res = GetById(id);

            if (res != null)
            {
                context.Reservations.Remove(res);
                context.SaveChanges();
            }
        }
    }
}