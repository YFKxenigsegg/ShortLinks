using System;
using System.ComponentModel.DataAnnotations;

namespace ShortLinks.Models.Entities
{
    public class Link
    {
        [Key]
        public string ShortLink { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime Created { get; set; }
        public string OriginalLink { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
