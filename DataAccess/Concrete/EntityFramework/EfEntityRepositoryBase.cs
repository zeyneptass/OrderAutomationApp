using DataAccess.Abstract;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using (CafeContext context = new CafeContext())
            {
                return filter == null
                    ? context.Set<T>().ToList()
                    : context.Set<T>().Where(filter).ToList();
            }
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            using (CafeContext context = new CafeContext())
            {
                return context.Set<T>().SingleOrDefault(filter);
            }
        }

        public void Add(T entity)
        {
            using (CafeContext context = new CafeContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (CafeContext context = new CafeContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (CafeContext context = new CafeContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public List<T> GetDetails(Expression<Func<T, bool>> filter)
        {
            using (CafeContext context = new CafeContext())
            {
                return context.Set<T>().Where(filter).ToList();
            }
        }
    }

}
