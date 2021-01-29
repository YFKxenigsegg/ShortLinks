using Microsoft.AspNetCore.Mvc;
using ShortLinks.Models.Entities;
using ShortLinks.BLL.Interfaces;
using System.Threading.Tasks;
using ShortLinks.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ShortLinks.Services;
using System.Linq;
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
        public IActionResult GetAll()
        {
            _logger.LogInfo("");
            _logger.LogDebug("Mapping to OutputLinkDTO from _linkService.GetAll(_userManagerService.GetUserId())");
            var links = _mapper.Map<IQueryable<OutputLinkDTO>>(_linkService.GetAll(_userManagerService.GetUserId()));
            _logger.LogDebug("Return Ok(links)");
            return Ok(links);
        }

        [HttpGet,Route("get")]
        public async Task<IActionResult> Get(InputLinkDTO lnk)
        {
            _logger.LogInfo("");
            _logger.LogDebug("Mapping InputLinkDTO to Link");
            var link = _mapper.Map<Link>(lnk);
            _logger.LogDebug("Getting result from LinkService.GetOne()");
            var resultLink = await _linkService.GetOne(link);
            _logger.LogDebug("Mapping Link to OutputLinkDTO");
            var outputLinkDto = _mapper.Map<OutputLinkDTO>(resultLink);
            _logger.LogDebug("Return Ok(outputLinkDto)");
            return Ok(outputLinkDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InputLinkDTO lnk)
        {
            _logger.LogInfo("");
            _logger.LogDebug("Mapping InputLinkDTO to Link");
            var link = _mapper.Map<Link>(lnk);
            _logger.LogDebug("Getting result from LinkService.GetUserId()");
            var resultLink = await _linkService.Create(link, _userManagerService.GetUserId());
            _logger.LogDebug("Mapping Link to OutputLinkDTO");
            var outputLinkDto = _mapper.Map<OutputLinkDTO>(resultLink);
            _logger.LogDebug("Return Ok(outputLinkDto)");
            return Ok(outputLinkDto);
        }

        [HttpPut]
        public async Task<IActionResult> Put(InputLinkDTO lnk)
        {
            _logger.LogInfo("");
            _logger.LogDebug("Mapping InputLinkDTO to Link");
            var link = _mapper.Map<Link>(lnk);
            _logger.LogDebug("Getting result from LinkService.Update()");
            await _linkService.Update(link);
            _logger.LogDebug("Return Ok(link)");
            return Ok(link);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(InputLinkDTO lnk)
        {
            _logger.LogInfo("");
            _logger.LogDebug("Mapping InputLinkDTO to Link");
            var link = _mapper.Map<Link>(lnk);
            _logger.LogDebug("Getting result from LinkService.Delete()");
            await _linkService.Delete(link);
            _logger.LogDebug("Return Ok()");
            return Ok();
        }
    }
}