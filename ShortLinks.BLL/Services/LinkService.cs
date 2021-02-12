using ShortLinks.BLL.Interfaces;
using ShortLinks.DAL.Interfaces;
using ShortLinks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ShortLinks.Models.DTO;
using ShortLinks.Models.Exceptions;

namespace ShortLinks.BLL.Services
{
    public class LinkService : ILinkService
    {
        private readonly IUnitOfWork _database;
        public LinkService(IUnitOfWork uow) { _database = uow; }
        public async Task<IEnumerable<Link>> GetAll(int idUser)
        {
            return await _database.Links.GetAll().Where(x => x.UserId == idUser).ToListAsync();
        }
        public async Task<Link> GetOne(Link lnk)
        {
            lnk.ShortLink = await CreateShortLink(lnk);
            var link = await _database.Links.Get(lnk.ShortLink);
            return link;
        }
        public async Task<Link> Create(Link link, int userid)
        {
            link.Created = DateTime.Now;
            link.ShortLink = await CreateShortLink(link);
            link.UserId = userid;
            var newLink = await _database.Links.Add(link);
            await _database.Save();
            return newLink;
        }
        public async Task<Link> Update(InputLinkPutDTO linkPut, int userid)
        {
            //new version of method - it's my solve, but I have old exception in EFUnitOfWork class which marked there
            //    For my case, the problem caused when I tried to pass to Update() method an entity that didn't exist in database.
            var link = new Link();
            link.OriginalLink = linkPut.MutableOriginalLink;
            link.ShortLink = await CreateShortLink(link);
            var isExist = await _database.Links.Get(link.ShortLink);
            if (isExist == null)
                throw new IncorrectDataException(HttpStatusCode.NotFound, "Link doesn't exist!");
            link.Created = DateTime.Now;
            link.UserId = userid;
            link.OriginalLink = linkPut.NewOriginalLink;
            link.ShortLink = await CreateShortLink(link);
            //  old version of method
            //link.Created = DateTime.Now;
            //link.ShortLink = await CreateShortLink(link);
            //link.UserId = userid;
            _database.Links.Update(link);
            await _database.Save();
            return link;
        }

        public async Task Delete(Link lnk)
        {
            var link = await _database.Links.Get(lnk.ShortLink);
            _database.Links.Delete(link);
            await _database.Save();
        }
        private async Task<string> CreateShortLink(Link lnk)
        {
            var md5 = MD5.Create();
            lnk.ShortLink = null;
            while (true)
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(lnk.OriginalLink));
                var str = Convert.ToBase64String(hash).Substring(0, 7);
                var link = await _database.Links.Get(lnk.ShortLink);
                if (link == null) return str;
            }
        }
    }
}
