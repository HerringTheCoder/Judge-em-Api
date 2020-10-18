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
                .HasForeignKey(cr => cr.CategoryId);
            modelBuilder.Entity<CategoryRating>()
                .HasOne(cr => cr.Rating)
                .WithMany(r => r.CategoryRatings)
                .HasForeignKey(cr => cr.RatingId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryRating> CategoryRatings { get; set; }
    }
}
