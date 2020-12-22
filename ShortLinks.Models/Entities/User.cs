using System.Collections.Generic;

namespace ShortLinks.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordCode { get; set; }
        public List<Link> Links { get; set; }
    }
}
