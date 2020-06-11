using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantOrderApp.Repositoriy
{
    interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
