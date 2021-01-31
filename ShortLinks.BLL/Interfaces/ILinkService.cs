﻿using System.Collections.Generic;
using ShortLinks.Models.Entities;
using System.Threading.Tasks;

namespace ShortLinks.BLL.Interfaces
{
    public interface ILinkService
    {
        public IEnumerable<Link> GetAll(int idUser);
        public Task<Link> GetOne(Link link);
        public Task<Link> Create(Link link, int idUser);
        public Task Update(Link link);
        public Task Delete(Link link);
    }
}
