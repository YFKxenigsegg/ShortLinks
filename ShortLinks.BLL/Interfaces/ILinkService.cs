using ShortLinks.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortLinks.BLL.Interfaces
{
    public interface ILinkService
    {
        public Task<IEnumerable<Link>> GetAll();
        public Task<Link> GetOne(Link link);
        public Task<Link> Create(Link link);
        public Task Update(Link link);
        public Task Delete(Link link);
    }
}
