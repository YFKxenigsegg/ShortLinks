using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShortLinks.BLL.Interfaces;
using ShortLinks.Models.DTO;
using ShortLinks.Models.Entities;

namespace ShortLinks.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService serv, IMapper mapper)
        {
            _accountService = serv;
            _mapper = mapper;
        }

        [HttpPost, Route("registration")]
        public async Task<IActionResult> Registrarion(AuthUserDTO usr)
        {
            var user = _mapper.Map<User>(usr);
            var resultuser = await _accountService.Registrarion(user);
            return Ok(resultuser);
        }
        
        [HttpPost, Route("login")]
        public async Task<IActionResult> Authorization(AuthUserDTO usr)
        {
            var user = _mapper.Map<User>(usr);
            var resultUser = await _accountService.Authorization(user);
            return Unauthorized(resultUser);
        }

        [HttpGet]
        public async Task<IActionResult> GetInfoUser(AuthUserDTO usr)
        {
            var user = _mapper.Map<User>(usr);
            var resultUser = await _accountService.GetUserInfo(user);
            return Ok(resultUser);
        }
    }
}