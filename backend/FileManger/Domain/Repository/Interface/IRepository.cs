using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        T GetFirst(Expression<Func<T, bool>> predicate);
        T Get(long id);
        ICollection<T> Get();
        ICollection<T> Get(Expression<Func<T, bool>> predicate);
        ICollection<T> Get(Expression<Func<T, bool>> predicate, int page, int size, Func<T, object> filterAttribute,
            bool descending);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        long Create(T Entity);
        void Update(T Entity);
    }
}
