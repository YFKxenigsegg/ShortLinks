using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShortLinks.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<int> Add(T entity);
        Task<int> Add(IList<T> entities);
        Task<int> Update(T entity);
        Task<int> Update(IList<T> entities);
        Task<int> Deleted(T entity);
        Task<T> GetOne(int? id);
        Task<List<T>> GetSome(Expression<Func<T, bool>> where);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy,
        bool ascending);
    }
}