using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ShortLinks.Auth.Common
{
    public class AuthOptions
    {
        public string ISSUER { get; set; }
        public  string AUDIENCE { get; set; }
        public  string KEY { get; set; }
        public  int LIFETIME { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}