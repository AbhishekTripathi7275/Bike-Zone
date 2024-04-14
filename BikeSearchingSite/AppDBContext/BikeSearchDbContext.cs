using BikeSearchingSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeSearchingSite.AppDBContext
{
    public class BikeSearchDbContext : IdentityDbContext<IdentityUser>
    {
        public BikeSearchDbContext(DbContextOptions<BikeSearchDbContext> options) : base(options)
        {
        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
