using ShortLinks.Models.Entities;
using System.Threading.Tasks;

namespace ShortLinks.BLL.Interfaces
{
    public interface IAccountService
    {
        public Task<User> Registrarion(User user);
        public Task<User> Authorization(User user);
        public Task<User> GetUserInfo(User user);
    }
}
