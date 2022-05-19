using DiplomStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DiplomStore.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
          
        }
        public DbSet<Tovars> tovars { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Titles> titles { get; set; }

    }
}
