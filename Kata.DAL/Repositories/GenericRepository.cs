using Kata.BL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kata.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //Internal Properties
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbset;


        // Constructor
        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        // CRUD Operations
        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbset.AddRange(entities);
        }

        public void Delete(T entity)
        {
            var entry = _context.Entry(entity);
            _dbset.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public void Update(T entity)
        {
            var entry = _context.Entry(entity);
            _dbset.Attach(entity);
            entry.State = EntityState.Modified;
        }


        // Get Operations
        public T GetSingleByCriteria(Expression<Func<T, bool>> wherePredicate)
        {
            return _dbset.SingleOrDefault(wherePredicate);
        }

        public IEnumerable<T> GetAllByCriteria(Expression<Func<T, bool>> wherePredicate, Expression<Func<T, int>> orderByPredicate)
        {
            return _dbset.Where(wherePredicate).OrderBy(orderByPredicate).ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, int>> orderByPredicate)
        {
            return _dbset.OrderBy(orderByPredicate).ToList();
        }

        public T GetById(Expression<Func<T, bool>> wherePredicate)
        {
            return _dbset.Where(wherePredicate).SingleOrDefault();
        }
    }
}
