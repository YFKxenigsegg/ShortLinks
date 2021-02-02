using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShortLinks.Auth.Common;
using ShortLinks.BLL.Interfaces;
using ShortLinks.Contracts;
using ShortLinks.Models.DTO;
using ShortLinks.Models.Entities;
using Newtonsoft.Json;

namespace ShortLinks.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        private readonly AuthOptions _appSettings;
        public AccountController(IAccountService serv, IMapper mapper, ILoggerManager logger, IOptions<AuthOptions> configuration)
        {
            _accountService = serv;
            _mapper = mapper;
            _logger = logger;
            _appSettings = configuration.Value;
        }
        [AllowAnonymous]
        [HttpPost, Route("registration")]
        public async Task<IActionResult> Registration(AuthUserDTO usr)
        {
            _logger.LogInfo("");
            _logger.LogDebug("Mapping AuthUserDTO to User");
            var user = _mapper.Map<User>(usr);
            _logger.LogDebug("Getting result from AccountService.Registration()");
            var resultUser = await _accountService.Registration(user);
            _logger.LogDebug("Return Ok(resultUser)");
            return Ok(resultUser);
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Authorization(AuthUserDTO usr)
        {
            _logger.LogInfo("");
            _logger.LogDebug("Mapping AuthUserDTO to User");
            var user = _mapper.Map<User>(usr);
            _logger.LogDebug("Getting result from AccountService.Authorization()");
            var resultUser = await _accountService.Authorization(user);
            _logger.LogDebug("Return Ok(resultUser.Token)");
            return Ok(resultUser.Token);
        }

        [HttpGet]
        public async Task<IActionResult> GetInfoUser(AuthUserDTO usr)
        {
            _logger.LogInfo("");
            _logger.LogDebug("Mapping AuthUserDTO to User");
            var user = _mapper.Map<User>(usr);
            _logger.LogDebug("Getting result from AccountService.GetUserInfo()");
            var resultUser = await _accountService.GetUserInfo(user);
            _logger.LogDebug("Return Ok(resultUser.Email)");
            return Ok(resultUser.Email);
        }

        //ВРЕМЕННЫЙ МЕТОД
        [AllowAnonymous]
        [HttpPost("/token")]
        public async Task<IActionResult> Token(AuthUserDTO usr)
        {
            var user = _mapper.Map<User>(usr);
            var identity = GetIdentity(user);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: _appSettings.ISSUER,
                audience: _appSettings.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_appSettings.LIFETIME)),
                signingCredentials: new SigningCredentials(_appSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Content(JsonConvert.SerializeObject(response));
        }
        private ClaimsIdentity GetIdentity(User usr)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", usr.UserId.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, usr.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}