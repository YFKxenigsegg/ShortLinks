using System;

namespace ShortLinks.Models.DTO
{
    public class InputLinkPutDTO
    {
        public DateTime ExpirationDate { get; set; }
        public string MutableOriginalLink { get; set; }
        public string NewOriginalLink { get; set; }
    }
}
