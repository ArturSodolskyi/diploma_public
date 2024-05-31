using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Reviews.Domain;


namespace Module.Reviews.Persistence.EntityConfigurations
{
    public class ReviewTaskConfiguration : IEntityTypeConfiguration<ReviewTask>
    {
        public void Configure(EntityTypeBuilder<ReviewTask> builder)
        {
            builder.HasKey(x => new { x.ReviewId, x.TaskId });

            builder.HasOne(x => x.Review)
                .WithMany(x => x.ReviewTasks)
                .HasForeignKey(x => x.ReviewId)
                .IsRequired();
        }
    }
}
