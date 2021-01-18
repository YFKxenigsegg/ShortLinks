using ShortLinks.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ShortLinks.BLL.Interfaces
{
    public interface ILinkService
    {
        public IQueryable<Link> GetAll(int idUser);
        public Task<Link> GetOne(Link link);
        public Task<Link> Create(Link link, int idUser);
        public Task Update(Link link);
        public Task Delete(Link link);
    }
}
