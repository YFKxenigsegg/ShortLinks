using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;;

namespace ShortLinks.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManagerService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            return int.Parse(GetUserDataFromClaims(_httpContextAccessor.HttpContext.User, "UserId"));
        }

        private string GetUserDataFromClaims(ClaimsPrincipal user, string typeClaimName)
        {
            IEnumerable<Claim> claims = ((ClaimsIdentity)user.Identity).Claims;

            string userData = claims.Where(r => r.Type == typeClaimName).Select(f => f.Value).FirstOrDefault();

            return userData;
        }
    }
}
