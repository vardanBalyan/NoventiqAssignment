using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoventiqAssignment.DAL.Entities;
using NoventiqAssignment.Framework.Constants;

namespace NoventiqAssignment.DAL.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>() {
                new() {Name = AppConstants.ADMIN, NormalizedName = AppConstants.ADMIN.ToUpper()},
                new() {Name = AppConstants.USER, NormalizedName = AppConstants.USER.ToUpper()}
            });
        }
    }
}
