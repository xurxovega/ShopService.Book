using Microsoft.EntityFrameworkCore;
using ShopService.Book.Domain.Entities;

namespace ShopService.Book.Infraestructure.Persistance
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<BookMaterial> Book  {get; set;}
    }
}
