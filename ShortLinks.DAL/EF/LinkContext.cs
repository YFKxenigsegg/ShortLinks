using ShortLinks.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShortLinks.DAL.EF
{
    public class LinkContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Link> Links { get; set; }
        public LinkContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=192.168.0.104;Port=5432;Database=shortlinks;Username=testUser;Password=testPassword");
        }
        public LinkContext(DbContextOptions<LinkContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
