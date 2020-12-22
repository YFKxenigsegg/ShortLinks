using ShortLinks.DAL.Interfaces;
using ShortLinks.DAL.EF;
using ShortLinks.Models.Entities;

namespace ShortLinks.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        private readonly LinkContext db;
        public UserRepository(LinkContext context) { db = context; }

    }
}
