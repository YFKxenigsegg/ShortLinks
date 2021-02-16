using System;

namespace ShortLinks.Models.DTO
{
    public class InputLinkCreateDTO
    {
        public DateTime ExpirationDate { get; set; }
        public string OriginalLink { get; set; }
    }
}
