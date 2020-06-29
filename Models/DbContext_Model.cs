using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Top_lista_vremena.Models
{
    public class DbContext_Model
    {
        public class AppDbContext : IdentityDbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            //public DbSet<TopTime> TopTimes { get; set; }
           // public DbSet<Login> LoginSet { get; set; }
            //public DbSet<Role> RoleSet { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }
    }
    
}
