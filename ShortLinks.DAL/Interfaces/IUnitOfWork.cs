using ShortLinks.Models.Entities;
using System;

namespace ShortLinks.DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Link> Links { get; }
        void Save();
    }
}
