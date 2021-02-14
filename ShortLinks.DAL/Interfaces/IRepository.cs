using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace ShortLinks.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> Get(int entityIdentifier);
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<List<T>> GetSome(Expression<Func<T, bool>> where);
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        public Task SaveChangesAsync();
    }
}