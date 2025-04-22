using Microsoft.EntityFrameworkCore;
using PruebaTecnicaPlatec.Domain.Entidades;

namespace PruebaTecnicaPlatec.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
