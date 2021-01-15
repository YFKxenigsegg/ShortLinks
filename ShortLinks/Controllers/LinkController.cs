using Microsoft.AspNetCore.Mvc;
using ShortLinks.Models.Entities;
using System.Collections.Generic;
using ShortLinks.BLL.Interfaces;
using System.Threading.Tasks;
using ShortLinks.Models.DTO;
using AutoMapper;

namespace ShortLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly IMapper _mapper;
        public LinkController(ILinkService serv, IMapper mapper)
        {
            _linkService = serv;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var links = _mapper.Map<IEnumerable<OutputLinkDTO>>(await _linkService.GetAll());
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
            var resultlink = await _linkService.Create(link);
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