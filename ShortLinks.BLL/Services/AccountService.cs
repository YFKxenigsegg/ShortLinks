using Microsoft.Extensions.Configuration;
using ShortLinks.BLL.Interfaces;
using ShortLinks.DAL.Interfaces;
using ShortLinks.Models.Entities;
using ShortLinks.Auth.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShortLinks.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _database;
        private readonly AuthOptions _appSettings;
        private readonly ITokenService _tokenService;

        public AccountService(IUnitOfWork uow, IConfiguration configuration, ITokenService tokenSercive)
        {
            _database = uow;
            _appSettings = configuration.GetSection("AuthOptions") as AuthOptions;
            _tokenService = tokenSercive;
        }
        public async Task<User> Registrarion(User user)
        {
            string hashCode = _tokenService.GeneratePassword(10);
            string encodedPassword = _tokenService.EncodePassword(user.PasswordCode, hashCode);
            user.PasswordCode = encodedPassword;
            user.PasswordHash = hashCode;
            await _database.Save();
            return user;
        }
        public async Task<User> Authorization(User usr)
        {
            var users = _database.Users.GetAll();       //!! посмотреть на metanit
            User user = await users.FirstOrDefaultAsync(r => r.Email == usr.Email);
            //if (user == null)
                //throw new IncorrectDataException("User doesn't exist!");

            // шифрование пароля по хеш-коду (salt method)
            string hashCode = user.PasswordHash;
            string encodingPasswordString = _tokenService.EncodePassword(usr.PasswordCode, hashCode);

            if(encodingPasswordString.Equals(user.PasswordCode))
            //    if (user == null)
            //        throw new IncorrectDataException("Incorrect password!");

            user.Token = _tokenService.GenerateJWT(user);
            await _database.Save();
            return user;
        }

        public async Task<User> GetUserInfo(User user)
        {
            return await _database.Users.Get(user);
        }
    }
}
