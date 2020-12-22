using ShortLinks.DAL.EF;
using ShortLinks.Models.Entities;

namespace ShortLinks.DAL.Repositories
{
    public class LinkRepository : RepositoryBase<Link>
    {
        private readonly LinkContext db;
        public LinkRepository(LinkContext context) { db = context; }
    
    }
}
