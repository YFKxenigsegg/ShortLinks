using ShortLinks.Models.Entities;
using System;
using System.Threading.Tasks;

namespace ShortLinks.DAL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Link> Links { get; }
        Task Save();
    }
}
