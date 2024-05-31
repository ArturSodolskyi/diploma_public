using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Reviews.Domain;


namespace Module.Reviews.Persistence.EntityConfigurations
{
    public class ReviewResultConfiguration : IEntityTypeConfiguration<ReviewResult>
    {
        public void Configure(EntityTypeBuilder<ReviewResult> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Review)
               .WithOne(x => x.ReviewResult)
               .HasForeignKey<ReviewResult>(x => x.Id);
        }
    }
}
