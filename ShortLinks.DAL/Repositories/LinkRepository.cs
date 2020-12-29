using ShortLinks.DAL.EF;
using ShortLinks.Models.Entities;

namespace ShortLinks.DAL.Repositories
{
    public class LinkRepository : RepositoryBase<Link>
    {
        public LinkRepository(LinkContext context) : base(context) { }
    
    }
}
