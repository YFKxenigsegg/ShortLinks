using System;

namespace ShortLinks.Models.DTO
{
    public class InputLinkDTO
    {
        public int Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string OriginalLink { get; set; }
    }
}
