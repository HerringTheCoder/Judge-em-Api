using Microsoft.EntityFrameworkCore;
using Storage.Tables;

namespace Storage
{
    public class JudgeContext : DbContext
    {
        public JudgeContext(DbContextOptions<JudgeContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryRating>()
                .HasKey(cr => new {cr.CategoryId, cr.RatingId});
            modelBuilder.Entity<CategoryRating>()
                .HasOne(cr => cr.Category)
                .WithMany(c => c.CategoryRatings)
                .HasForeignKey(cr => cr.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CategoryRating>()
                .HasOne(cr => cr.Rating)
                .WithMany(r => r.CategoryRatings)
                .HasForeignKey(cr => cr.RatingId);

            modelBuilder.Entity<PlayerProfile>()
                .HasOne(pp => pp.Game)
                .WithMany(g => g.PlayerProfiles)
                .HasForeignKey(pp => pp.GameId);
            modelBuilder.Entity<PlayerProfile>()
                .HasOne(pp => pp.User)
                .WithMany(u => u.PlayerProfiles)
                .HasForeignKey(pp => pp.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PlayerProfile>()
                .HasMany(pp => pp.Ratings)
                .WithOne(r => r.PlayerProfile)
                .HasForeignKey(r => r.PlayerProfileId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryRating> CategoryRatings { get; set; }
        public DbSet<PlayerProfile> PlayerProfiles { get; set; }
    }
}
