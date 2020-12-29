using ShortLinks.BLL.Interfaces;
using ShortLinks.DAL.Interfaces;
using ShortLinks.Models.Entities;
using System.Threading.Tasks;

namespace ShortLinks.BLL.Services
{
    public class LinkService : ILinkService
    {
        IUnitOfWork Database { get; set; }
        public LinkService(IUnitOfWork uow) { Database = uow; }

        public async Task<Link> Get(string shrtlnk)
        {
            var link = await Database.Links.GetOne(shrtlnk);
            return link;
        }
    }
}
