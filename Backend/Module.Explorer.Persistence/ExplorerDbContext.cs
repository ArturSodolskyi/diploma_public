using Microsoft.EntityFrameworkCore;
using Module.Explorer.Domain;

namespace Module.Explorer.Persistence
{
    public class ExplorerDbContext(DbContextOptions<ExplorerDbContext> options) : DbContext(options), IExplorerDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Competence> Competencies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<CompetenceTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("explorer");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExplorerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
