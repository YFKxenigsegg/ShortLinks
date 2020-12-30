using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShortLinks.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task Add(IList<T> entities);
        Task Update(T entity);
        Task Update(IList<T> entities);
        Task Deleted(T entity);
        Task<T> GetOne(string shrtlnk);
        Task<List<T>> GetSome(Expression<Func<T, bool>> where);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy,
        bool ascending);
        public Task SaveChangesAsync();
    }
}