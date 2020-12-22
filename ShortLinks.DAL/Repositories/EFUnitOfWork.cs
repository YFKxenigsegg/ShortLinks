using Microsoft.EntityFrameworkCore;
using ShortLinks.DAL.EF;
using ShortLinks.Models.Entities;
using ShortLinks.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace ShortLinks.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private LinkContext db;
        private UserRepository userRepository;
        private LinkRepository linkRepository;
        public EFUnitOfWork(DbContextOptions<LinkContext> options)
        {
            db = new LinkContext(options);
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }
        public IRepository<Link> Links
        {
            get
            {
                if (linkRepository == null)
                    linkRepository = new LinkRepository(db);
                return linkRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;

        public async Task Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await db.DisposeAsync();
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