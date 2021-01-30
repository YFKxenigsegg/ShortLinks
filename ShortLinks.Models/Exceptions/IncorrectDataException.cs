using System;
using System.Net;

namespace ShortLinks.Models.Exceptions
{
    public class IncorrectDataException : Exception
    {
        public HttpStatusCode Status { get; private set; }
        public IncorrectDataException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }

    }
}
