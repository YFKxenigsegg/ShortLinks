using Microsoft.AspNetCore.Mvc;
using ShortLinks.Models.Entities;
using System.Collections.Generic;
using ShortLinks.BLL.Interfaces;
using System.Threading.Tasks;

namespace ShortLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        ILinkService linkService;
        public LinkController(ILinkService serv) { linkService = serv; }
        [HttpGet]
        public async Task<IEnumerable<Link>> Get()
        {
            return await linkService.GetAll();
        }

        [HttpGet("{shrtlnk}")]
        public async Task<ActionResult<Link>> Get(string shrtlnk)
        {
            var link = await linkService.GetOne(shrtlnk);
            if (link == null)
                return NotFound();
            return Ok(link);
        }
    }
}