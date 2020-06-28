using Microsoft.EntityFrameworkCore;

namespace Top_lista_vremena.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        public DbSet<TopTime> TopTimeDbSet { get; set; }
    }
}
