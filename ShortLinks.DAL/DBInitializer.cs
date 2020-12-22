using ShortLinks.DAL.EF;
using ShortLinks.Models.Entities;
using System.Linq;

namespace ShortLinks.DAL
{
    public class DBInitializer
    {
        public static void Initial(LinkContext context)
        {
            if (!context.Users.Any())
            {
                context.AddRange(
                    new User { Email = "firstemail", PasswordCode = "firstpasswordcode", PasswordHash = "firstpasswordhash", Links = null },
                new User { Email = "secondemail", PasswordCode = "secondpasswordcode", PasswordHash = "secondpasswordhash", Links = null });
            }
            context.SaveChanges();
        }
    }
}
