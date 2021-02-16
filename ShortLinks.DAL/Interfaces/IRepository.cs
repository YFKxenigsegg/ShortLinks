using System.Threading.Tasks;
using System.Linq;

namespace ShortLinks.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> Get(int entitySearch);
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}