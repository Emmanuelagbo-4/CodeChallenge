using CodeChallenge.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers {get; set;}
        public DbSet<Like> Likes {get; set;}
        public DbSet<Post> Posts {get; set;}
    }
}