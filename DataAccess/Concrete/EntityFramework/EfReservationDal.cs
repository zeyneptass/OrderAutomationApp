using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReservationDal : IRezervationDal
    {
        public void Add(Reservation entity)
        {
            using (CafeContext context = new CafeContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Reservation entity)
        {
            using (CafeContext context = new CafeContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Reservation Get(Expression<Func<Reservation, bool>> filter)
        {
            using (CafeContext context = new CafeContext())
            {
                return context.Set<Reservation>().SingleOrDefault(filter);
            }
        }

        public List<Reservation> GetAll(Expression<Func<Reservation, bool>> filter = null)
        {
            using (CafeContext context = new CafeContext())
            {
                return filter == null
                    ? context.Set<Reservation>().ToList() : context.Set<Reservation>().Where(filter).ToList();

            }
        }

        public void Update(Reservation entity)
        {
            using (CafeContext context = new CafeContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
