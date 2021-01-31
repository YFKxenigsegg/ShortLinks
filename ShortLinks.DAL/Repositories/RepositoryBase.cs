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
        public virtual IQueryable<T> GetAll() => _table.AsQueryable();
        public async Task<T> Get(T entity) => await _table.FindAsync(entity);
        public async Task<T> Add(T entity)
        {
            var newItem = await _table.AddAsync(entity);
            return newItem.Entity;
        }
        public void Update(T entity)
        {
            _table.Update(entity);
        }
        public void Delete(T entity)
        {
            _db.Remove(entity);
        }
        public async Task<List<T>> GetSome(Expression<Func<T, bool>> where)
        => await _table.Where(where).ToListAsync();
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate) => _table.Where(predicate);
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}