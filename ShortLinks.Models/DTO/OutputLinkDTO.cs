﻿using System;

namespace ShortLinks.Models.DTO
{
    public class OutputLinkDTO
    {
        public string ShortLink { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string OriginalLink { get; set; }
    }
}