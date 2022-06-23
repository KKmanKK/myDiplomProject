using DiplomStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiplomStore.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser,IdentityRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
          Database.EnsureCreated();
        }
        public DbSet<Tovars> tovars { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Titles> titles { get; set; }
        public DbSet<Order> orders { get; set; }

    }
}
