using ShortLinks.Models.Entities;
using System;

namespace ShortLinks.DAL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Link> Links { get; }
        void Save();
    }
}
