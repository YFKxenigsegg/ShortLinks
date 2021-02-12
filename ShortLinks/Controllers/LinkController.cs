using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShortLinks.Models.Entities;
using ShortLinks.BLL.Interfaces;
using System.Threading.Tasks;
using ShortLinks.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ShortLinks.Services;
using ShortLinks.Contracts;

namespace ShortLinks.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly IMapper _mapper;
        private readonly IUserManagerService _userManagerService;
        private readonly ILoggerManager _logger;
        public LinkController(ILinkService serv, IMapper mapper, IUserManagerService userManagerServ, ILoggerManager logger)
        {
            _linkService = serv;
            _mapper = mapper;
            _userManagerService = userManagerServ;
            _logger = logger;
        }
        [HttpGet, Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInfo("");
            var links = await _linkService.GetAll(_userManagerService.GetUserId());
            var linksAll = _mapper.Map<IEnumerable<OutputLinkDTO>>(links);
            return Ok(linksAll);
        }

        [HttpGet, Route("get")]
        public async Task<IActionResult> Get([FromQuery] InputLinkDTO lnk)
        {
            _logger.LogInfo("");
            var link = _mapper.Map<Link>(lnk);
            var resultLink = await _linkService.GetOne(link);
            var outputLinkDto = _mapper.Map<OutputLinkDTO>(resultLink);
            return Ok(outputLinkDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InputLinkDTO lnk)
        {
            _logger.LogInfo("");
            var link = _mapper.Map<Link>(lnk);
            var resultLink = await _linkService.Create(link, _userManagerService.GetUserId());
            var outputLinkDto = _mapper.Map<OutputLinkDTO>(resultLink);
            return Ok(outputLinkDto);
        }

        [HttpPut]
        public async Task<IActionResult> Put(InputLinkPutDTO lnk)
        {
            _logger.LogInfo("");
            //var link = _mapper.Map<Link>(lnk);
            var somelnk = await _linkService.Update(lnk, _userManagerService.GetUserId());
            return Ok(somelnk);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(OutputLinkDTO lnk)
        {
            _logger.LogInfo("");
            var link = _mapper.Map<Link>(lnk);
            await _linkService.Delete(link);
            return Ok();
        }
    }
}