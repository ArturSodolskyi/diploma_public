using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Explorer.Domain;

namespace Module.Explorer.Persistence.EntityConfigurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<CompetenceTask>
    {
        public void Configure(EntityTypeBuilder<CompetenceTask> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Competence)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.CompetenceId)
                .IsRequired();
        }
    }
}
