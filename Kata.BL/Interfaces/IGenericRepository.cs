using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Kata.BL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // CRUD Operations
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        void Remove(T entity);
        void Update(T entity);

        // Get Operations
        T GetSingleByCriteria(Expression<Func<T, bool>> wherePredicate);
        IEnumerable<T> GetAllByCriteria(Expression<Func<T, bool>> wherePredicate, Expression<Func<T, int>> orderByPredicate);
        IEnumerable<T> GetAll(Expression<Func<T, int>> orderByPredicate);
        //IEnumerable<T> GetAll(Expression<Func<T, string>> orderByPredicate);
        T GetById(Expression<Func<T, bool>> wherePredicate);
    }
}
