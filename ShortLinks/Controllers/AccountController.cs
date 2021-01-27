using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortLinks.BLL.Interfaces;
using ShortLinks.Contracts;
using ShortLinks.Models.DTO;
using ShortLinks.Models.Entities;

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
        public AccountController(IAccountService serv, IMapper mapper, ILoggerManager logger)
        {
            _accountService = serv;
            _mapper = mapper;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpPost, Route("registration")]
        public async Task<IActionResult> Registration(AuthUserDTO usr)
        {
            _logger.LogInfo("Registration user");
            _logger.LogDebug("Registration. Mapping AuthUserDTO to User");
            var user = _mapper.Map<User>(usr);
            _logger.LogDebug("Registration. Getting result from AccountService.Registration()");
            var resultUser = await _accountService.Registrarion(user);
            _logger.LogDebug("Registration. Return Ok(resultUser)");
            return Ok(resultUser);
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Authorization(AuthUserDTO usr)
        {
            _logger.LogInfo("Authorization user");
            _logger.LogDebug("Authorization. Mapping AuthUserDTO to User");
            var user = _mapper.Map<User>(usr);
            _logger.LogDebug("Authorization. Getting result from AccountService.Authorization()");
            var resultUser = await _accountService.Authorization(user);
            _logger.LogDebug("Authorization. Return Ok(resultUser.Token)");
            return Ok(resultUser.Token);
        }

        [HttpGet]
        public async Task<IActionResult> GetInfoUser(AuthUserDTO usr)
        {
            _logger.LogInfo("GetInfoUser action");
            _logger.LogDebug("GetInfoUser. Mapping AuthUserDTO to User");
            var user = _mapper.Map<User>(usr);
            _logger.LogDebug("GetInfoUser. Getting result from AccountService.GetUserInfo()");
            var resultUser = await _accountService.GetUserInfo(user);
            _logger.LogDebug("GetInfoUser. Return Ok(resultUser.Email)");
            return Ok(resultUser.Email);
        }
    }
}