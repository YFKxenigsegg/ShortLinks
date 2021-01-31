using ShortLinks.DAL.EF;
using ShortLinks.Models.Entities;

namespace ShortLinks.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(LinkContext context) : base(context) { }
    }
}
