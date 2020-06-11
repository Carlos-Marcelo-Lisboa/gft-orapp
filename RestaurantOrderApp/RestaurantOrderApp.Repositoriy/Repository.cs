using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using RestaurantOrderApp.DAL;

namespace RestaurantOrderApp.Repositoriy
{
    public abstract class Repository<T> : IRepository<T> where T:class
    {
        protected RestaurantOrderAppContext ctx;

        public Repository(RestaurantOrderAppContext context)
        {
            ctx = context;
        }

        public T Add(T entity)
        {
            return ctx.Add(entity).Entity;
        }

        public T Delete(T entity)
        {
            return ctx.Remove(entity).Entity;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return ctx.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public void SaveChanges()
        {
            ctx.SaveChanges();
        }

        public T Update(T entity)
        {
            return ctx.Update(entity).Entity;
        }
    }
}
