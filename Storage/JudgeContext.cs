using Microsoft.EntityFrameworkCore;
using Storage.Tables;

namespace Storage
{
    public class JudgeContext : DbContext
    {
        public JudgeContext(DbContextOptions<JudgeContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
