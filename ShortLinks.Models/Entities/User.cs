
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortLinks.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordCode { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
