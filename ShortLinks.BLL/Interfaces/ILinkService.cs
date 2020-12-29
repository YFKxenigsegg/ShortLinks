using ShortLinks.Models.Entities;
using System.Threading.Tasks;

namespace ShortLinks.BLL.Interfaces
{
    public interface ILinkService
    {
        public Task<Link> Get(string shrtlnk);
    }
}
