using ShortLinks.DAL.Interfaces;
using ShortLinks.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ShortLinks.DAL.Repositories
{
    public class RepositoryBase<T> : IDisposable, IRepository<T> where T : class
    {
        private readonly DbSet<T> _table;
        private readonly LinkContext _db;
        public RepositoryBase() { }
        public RepositoryBase(LinkContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }
        public void Dispose()
        {
            _db?.Dispose();
        }
        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }
        public int Add(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }
        public int Update(T entity)
        {
            _table.Update(entity);
            return SaveChanges();
        }
        public int Update(IList<T> entities)
        {
            _table.UpdateRange(entities);
            return SaveChanges();
        }
        public int Deleted(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }
        public T GetOne(int? id) => _table.Find(id);
        public virtual List<T> GetAll() => _table.ToList();

        public List<T> GetAll<TSortField>(Expression<Func<T, TSortField>>
            orderBy, bool ascending) => (ascending ? _table.OrderBy(orderBy) :
            _table.OrderByDescending(orderBy)).ToList();
        public List<T> GetSome(Expression<Func<T, bool>> where)
        => _table.Where(where).ToList();
        internal int SaveChanges()
        {
            return _db.SaveChanges();
        }
    }
}
