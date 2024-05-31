using Microsoft.EntityFrameworkCore;
using Module.Explorer.Domain;

namespace Module.Explorer.Persistence
{
    public interface IExplorerDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Competence> Competencies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<CompetenceTask> Tasks { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
