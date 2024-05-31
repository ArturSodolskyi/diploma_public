using Microsoft.EntityFrameworkCore;
using Module.Reviews.Domain;

namespace Module.Reviews.Persistence
{
    public interface IReviewsDbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewResult> ReviewResults { get; set; }
        public DbSet<ReviewTask> ReviewTasks { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
