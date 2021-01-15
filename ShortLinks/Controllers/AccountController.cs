using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShortLinks.BLL.Interfaces;
using ShortLinks.DAL.EF;
using ShortLinks.Auth.Common;
using ShortLinks.Models.DTO;
using ShortLinks.Models.Entities;
using Microsoft.Extensions.Options;
using AutoMapper.Configuration;

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
        public async Task<IActionResult> Authorization(AuthUserDTO usr)//<-- минимум логики!!
        {
            var user = _mapper.Map<User>(usr);
            var resultUser = await _accountService.Authorization(user);
            if (resultUser != null)
            {
                //var token = GenerateJWT(resultUser);
                //return Ok(new { access_token = token });
            }
            return Unauthorized();
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