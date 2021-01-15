using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace ShortLinks.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll();
        Task<T> Get(T entity);
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<List<T>> GetSome(Expression<Func<T, bool>> where);
        Task<List<T>> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy,
        bool ascending);
        public Task SaveChangesAsync();
    }
}