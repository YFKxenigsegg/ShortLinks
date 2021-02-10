using ShortLinks.BLL.Interfaces;
using ShortLinks.DAL.Interfaces;
using ShortLinks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
            var link = await _database.Links.Get(lnk.OriginalLink);
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
        public async Task Update(Link link, int userid)
        {
            link.Created = DateTime.Now;
            link.ShortLink = await CreateShortLink(link);
            link.UserId = userid;
            _database.Links.Update(link);
            await _database.Save();
        }

        public async Task Delete(Link lnk)
        {
            var link = await _database.Links.Get(lnk.OriginalLink);
            _database.Links.Delete(link);
            await _database.Save();
        }
        private async Task<string> CreateShortLink(Link lnk)
        {
            var md5 = MD5.Create();
            while (true)
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(lnk.OriginalLink));
                var str = Encoding.Default.GetString(hash).Substring(0, 7);
                var link = await _database.Links.Get(lnk.OriginalLink);
                if (link == null) return str;
            }
        }
    }
}
