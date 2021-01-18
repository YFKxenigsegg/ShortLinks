using ShortLinks.Models.Entities;

namespace ShortLinks.BLL.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJWT(User user);
        public string GeneratePassword(int saltLength);
        public string EncodePassword(string pass, string salt);
    }
}
