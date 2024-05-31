using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Explorer.Domain;

namespace Module.Explorer.Persistence.EntityConfigurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Category)
               .WithMany(x => x.Jobs)
               .HasForeignKey(x => x.CategoryId);
        }
    }
}
