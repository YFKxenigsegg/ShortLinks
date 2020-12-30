using ShortLinks.BLL.Interfaces;
using ShortLinks.DAL.Interfaces;
using ShortLinks.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortLinks.BLL.Services
{
    public class LinkService : ILinkService
    {
        private readonly IUnitOfWork _database;
        public LinkService(IUnitOfWork uow) { _database = uow; }
        public async Task<IEnumerable<Link>> GetAll()
        {
            return await _database.Links.GetAll();
        }
        public async Task<Link> GetOne(string shrtlnk)
        {
            var link = await _database.Links.GetOne(shrtlnk);
            return link;
        }
    }
}
