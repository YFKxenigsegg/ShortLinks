﻿using System;

namespace ShortLinks.Models.DTO
{
    public class InputLinkDTO
    {
        public DateTime ExpirationDate { get; set; }
        public string ShortLink { get; set; }
    }
}