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
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInfo("GetAll action");
            _logger.LogDebug("GetAll. Mapping to OutputLinkDTO from _linkService.GetAll(_userManagerService.GetUserId())");
            var links = _mapper.Map<IQueryable<OutputLinkDTO>>(_linkService.GetAll(_userManagerService.GetUserId()));
            _logger.LogDebug("GetAll. Return Ok(links)");
            return Ok(links);
        }

        [HttpGet]
        public async Task<IActionResult> Get(InputLinkDTO lnk)
        {
            _logger.LogInfo("Get action");
            _logger.LogDebug("Get. Mapping InputLinkDTO to Link");
            var link = _mapper.Map<Link>(lnk);
            _logger.LogDebug("Get. Getting result from LinkService.GetOne()");
            var resultLink = await _linkService.GetOne(link);
            _logger.LogDebug("Get. Mapping Link to OutputLinkDTO");
            var outputLinkDto = _mapper.Map<OutputLinkDTO>(resultLink);
            _logger.LogDebug("Get. Return Ok(outputLinkDto)");
            return Ok(outputLinkDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InputLinkDTO lnk)
        {
            _logger.LogInfo("Post action");
            _logger.LogDebug("Post. Mapping InputLinkDTO to Link");
            var link = _mapper.Map<Link>(lnk);
            _logger.LogDebug("Post. Getting result from LinkService.GetUserId()");
            var resultLink = await _linkService.Create(link, _userManagerService.GetUserId());
            _logger.LogDebug("Post. Mapping Link to OutputLinkDTO");
            var outputLinkDto = _mapper.Map<OutputLinkDTO>(resultLink);
            _logger.LogDebug("Get. Return Ok(outputLinkDto)");
            return Ok(outputLinkDto);
        }

        [HttpPut]
        public async Task<IActionResult> Put(InputLinkDTO lnk)
        {
            _logger.LogInfo("Put action");
            _logger.LogDebug("Put. Mapping InputLinkDTO to Link");
            var link = _mapper.Map<Link>(lnk);
            _logger.LogDebug("Put. Getting result from LinkService.Update()");
            await _linkService.Update(link);
            _logger.LogDebug("Put. Return Ok(link)");
            return Ok(link);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(InputLinkDTO lnk)
        {
            _logger.LogInfo("Delete action");
            _logger.LogDebug("Delete. Mapping InputLinkDTO to Link");
            var link = _mapper.Map<Link>(lnk);
            _logger.LogDebug("Delete. Getting result from LinkService.Delete()");
            await _linkService.Delete(link);
            _logger.LogDebug("Put. Return Ok()");
            return Ok();
        }
    }
}