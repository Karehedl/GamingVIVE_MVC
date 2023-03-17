using GamingVIVE.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamingVIVE_MVC.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Game> Games { get; set; }
        public DbSet<User> AppUsers { get; set; }
        public DbSet<GamingSystem> GamingSystems { get; set; }

        public DbSet<GamingSystemGame> GamingSystemGames { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<GenreGame> GenreGames { get; set; }
        public DbSet<MontizedCategory> MontizedCategories { get; set; }

        public DbSet<MontizedCategoryGame> MontizedCategoryGames { get; set; }
        public DbSet<UserGame> UserGames { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                 new IdentityRole
                 {
                     Name = "Administrator",
                     NormalizedName = "ADMINSTRATOR"
                 },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }

            );

        }
    }
}