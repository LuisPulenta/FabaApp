using Microsoft.EntityFrameworkCore;
using FabaApp.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FabaApp.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {

        #region Constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        #endregion

        public DbSet<LabEntity> Labs { get; set; }
        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<RecipeDetailEntity> RecipeDetails { get; set; }
        public DbSet<SocialWorkEntity> SocialWorks { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<CodeEntity> Codes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LabEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SocialWorkEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }

    }
}