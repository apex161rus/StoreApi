using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Api.Model;

namespace Api.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions options)
        : base(options)
        {
            
        }

        public DbSet<AppUser> AppUsers {get; set;}
        public DbSet<Product> Products {get; set;}
    }
}