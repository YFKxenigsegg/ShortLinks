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
        public async Task<IActionResult> Registrarion(AuthUserDTO usr)
        {
            var user = _mapper.Map<User>(usr);
            var resultuser = await _accountService.Registrarion(user);
            return Ok(resultuser);
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Authorization(AuthUserDTO usr)
        {
            var user = _mapper.Map<User>(usr);
            var resultUser = await _accountService.Authorization(user);
            return Ok(resultUser.Token);
        }

        [HttpGet]
        public async Task<IActionResult> GetInfoUser(AuthUserDTO usr)
        {
            var user = _mapper.Map<User>(usr);
            var resultUser = await _accountService.GetUserInfo(user);
            return Ok(resultUser.Email);
        }
    }
}