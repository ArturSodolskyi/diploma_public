using Microsoft.EntityFrameworkCore;
using Module.Reviews.Domain;

namespace Module.Reviews.Persistence
{
    public class ReviewsDbContext(DbContextOptions<ReviewsDbContext> options) : DbContext(options), IReviewsDbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewResult> ReviewResults { get; set; }
        public DbSet<ReviewTask> ReviewTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("reviews");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReviewsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
