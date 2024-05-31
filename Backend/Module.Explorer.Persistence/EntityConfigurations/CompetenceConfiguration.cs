using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Explorer.Domain;

namespace Module.Explorer.Persistence.EntityConfigurations
{
    public class CompetenceConfiguration : IEntityTypeConfiguration<Competence>
    {
        public void Configure(EntityTypeBuilder<Competence> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Job)
               .WithMany(x => x.Competencies)
               .HasForeignKey(x => x.JobId)
               .IsRequired();
        }
    }
}
