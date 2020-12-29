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
    public abstract class RepositoryBase<T> : IAsyncDisposable, IRepository<T> where T : class
    {
        private readonly DbSet<T> _table;
        private readonly LinkContext _db;
        public RepositoryBase() { }
        public RepositoryBase(LinkContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }
        public async Task Dispose()
        {
            await _db.DisposeAsync();
        }
        public async ValueTask DisposeAsync()
        {
            await Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> Add(T entity)
        {
            await _table.AddAsync(entity);
            return await SaveChangesAsync();
        }
        public async Task<int> Add(IList<T> entities)
        {
            await _table.AddRangeAsync(entities);
            return await SaveChangesAsync();
        }
        public async Task<int> Update(T entity)
        {
            _table.Update(entity);
            return await SaveChangesAsync();
        }
        public async Task<int> Update(IList<T> entities)
        {
            _table.UpdateRange(entities);
            return await SaveChangesAsync();
        }
        public async Task<int> Deleted(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }
        public async Task<T> GetOne(int? id) => await _table.FindAsync(id);
        public virtual async Task<List<T>> GetAll() => await _table.ToListAsync();

        public async Task<List<T>> GetAll<TSortField>(Expression<Func<T, TSortField>>
            orderBy, bool ascending) => await (ascending ? _table.OrderBy(orderBy) :
            _table.OrderByDescending(orderBy)).ToListAsync();
        public async Task<List<T>> GetSome(Expression<Func<T, bool>> where)
        => await _table.Where(where).ToListAsync();
        internal Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}