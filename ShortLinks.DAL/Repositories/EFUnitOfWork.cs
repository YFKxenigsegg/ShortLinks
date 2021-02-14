using Microsoft.EntityFrameworkCore;
using ShortLinks.DAL.EF;
using ShortLinks.Models.Entities;
using ShortLinks.DAL.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ShortLinks.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly LinkContext _db;
        private UserRepository _userRepository;
        private LinkRepository _linkRepository;
        public EFUnitOfWork(DbContextOptions<LinkContext> options, IConfiguration configuration)
        {
            _db = new LinkContext(options, configuration);
        }
        public IRepository<User> Users
        {
            get { return _userRepository ??= new UserRepository(_db); }
        }
        public IRepository<Link> Links
        {
            get { return _linkRepository ??= new LinkRepository(_db); }
        }
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
        private bool disposed = false;

        public async Task Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await _db.DisposeAsync();
                }
                this.disposed = true;
            }
        }
        public async ValueTask DisposeAsync()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}