using Microsoft.AspNetCore.Mvc;
using ShortLinks.Models.Entities;
using ShortLinks.BLL.Interfaces;
using System.Threading.Tasks;
using ShortLinks.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ShortLinks.Services;
using System.Linq;

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
        public LinkController(ILinkService serv, IMapper mapper, IUserManagerService userManagerServ)
        {
            _linkService = serv;
            _mapper = mapper;
            _userManagerService = userManagerServ;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var links = _mapper.Map<IQueryable<OutputLinkDTO>>(_linkService.GetAll(_userManagerService.GetUserId()));
            return Ok(links);
        }

        [HttpGet]
        public async Task<IActionResult> Get(InputLinkDTO lnk)
        {
            var link = _mapper.Map<Link>(lnk);
            var resultlink = await _linkService.GetOne(link);
            var outputLinkDto = _mapper.Map<OutputLinkDTO>(resultlink);
            return Ok(outputLinkDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InputLinkDTO lnk)
        {
            var link = _mapper.Map<Link>(lnk);
            var resultlink = await _linkService.Create(link, _userManagerService.GetUserId());
            var outputLinkDto = _mapper.Map<OutputLinkDTO>(resultlink);
            return Ok(outputLinkDto);
        }

        [HttpPut]
        public async Task<IActionResult> Put(InputLinkDTO lnk)
        {
            var link = _mapper.Map<Link>(lnk);
            await _linkService.Update(link);
            return Ok(link);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(InputLinkDTO lnk)
        {
            var link = _mapper.Map<Link>(lnk);
            await _linkService.Delete(link);
            return Ok();
        }
    }
}