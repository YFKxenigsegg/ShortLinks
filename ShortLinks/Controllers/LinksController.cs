using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLinks.DAL.EF;
using ShortLinks.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        LinkContext db;
        public LinksController(LinkContext context) { db = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Link>>> Get()
        {
            return await db.Links.ToListAsync();
        }

        [HttpGet("{shrtlnk}")]
        public async Task<ActionResult<Link>> Get(string shrtlnk)
        {
            Link link = await db.Links.FirstOrDefaultAsync(x => x.ShortLink == shrtlnk);
            if (link == null)
                return NotFound();
            return new ObjectResult(link);
        }

        #region later
        [HttpPost]
        public async Task<ActionResult<Link>> Post(Link link)
        {
            if (link == null)
            {
                return BadRequest();
            }

            db.Links.Add(link);
            await db.SaveChangesAsync();
            return Ok(link);
        }

        [HttpPut]
        public async Task<ActionResult<Link>> Put(Link link)
        {
            if (link == null)
            {
                return BadRequest();
            }
            if (!db.Links.Any(x => x.ShortLink == link.ShortLink))
            {
                return NotFound();
            }

            db.Update(link);
            await db.SaveChangesAsync();
            return Ok(link);
        }

        [HttpDelete("{shrtlnk}")]
        public async Task<ActionResult<Link>> Delete(string shrtlnk)
        {
            Link link = db.Links.FirstOrDefault(x => x.ShortLink == shrtlnk);
            if (link == null)
            {
                return NotFound();
            }
            db.Links.Remove(link);
            await db.SaveChangesAsync();
            return Ok(link);
        }
        #endregion
    }
}