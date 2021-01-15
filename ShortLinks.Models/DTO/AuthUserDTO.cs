using System.ComponentModel.DataAnnotations;

namespace ShortLinks.Models.DTO
{
    public class AuthUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}