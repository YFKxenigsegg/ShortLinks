using ShortLinks.DAL.Interfaces;
using ShortLinks.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShortLinks.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _table;
        private readonly LinkContext _db;
        public RepositoryBase(LinkContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }
        public async Task Add(T entity)
        {
            await _table.AddAsync(entity);
        }
        public async Task Add(IList<T> entities)
        {
            await _table.AddRangeAsync(entities);
        }
        public async Task Update(T entity)
        {
            _table.Update(entity);
        }
        public async Task Update(IList<T> entities)
        {
            _table.UpdateRange(entities);
        }
        public async Task Deleted(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
        }
        public async Task<T> GetOne(string shrtlnk) => await _table.FindAsync(shrtlnk);
        public virtual async Task<List<T>> GetAll() => await _table.ToListAsync();

        public async Task<List<T>> GetAll<TSortField>(Expression<Func<T, TSortField>>
            orderBy, bool ascending) => await (ascending ? _table.OrderBy(orderBy) :
            _table.OrderByDescending(orderBy)).ToListAsync();
        public async Task<List<T>> GetSome(Expression<Func<T, bool>> where)
        => await _table.Where(where).ToListAsync();
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}