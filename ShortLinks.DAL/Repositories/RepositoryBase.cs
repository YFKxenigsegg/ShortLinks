using ShortLinks.DAL.Interfaces;
using ShortLinks.DAL.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ShortLinks.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _table;
        private readonly LinkContext _db;
        protected RepositoryBase(LinkContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }
        public virtual IQueryable<T> GetAll() => _table.AsQueryable();
        public async Task<T> Get(int entitySearch) => await _table.FindAsync(entitySearch);
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
    }
}