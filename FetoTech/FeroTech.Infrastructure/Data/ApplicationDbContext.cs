using FeroTech.Infrastructure.Domain.Entities;
using FeroTech.Infrastructure.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeroTech.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Fine> Fine { get; internal set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
