using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLinks.DAL.EF;
using ShortLinks.Models.Entities;
using System.Collections.Generic;
using ShortLinks.BLL.Interfaces;
using System.Threading.Tasks;

namespace ShortLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        LinkContext db;
        ILinkService linkService;
        public LinksController(ILinkService serv, LinkContext context) {
            db = context;
            linkService = serv; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Link>>> Get()
        {
            return await db.Links.ToListAsync();
        }

        [HttpGet("{shrtlnk}")]
        public async Task<ActionResult<Link>> Get(string shrtlnk)
        {
            var link = await linkService.Get(shrtlnk);
            if (link == null)
                return NotFound();
            return new ObjectResult(link);
        }
    }
}