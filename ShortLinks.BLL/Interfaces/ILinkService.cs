using ShortLinks.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortLinks.BLL.Interfaces
{
    public interface ILinkService
    {
        //other methods
        public Task<IEnumerable<Link>> GetAll();
        public Task<Link> GetOne(string shrtlnk);
    }
}
