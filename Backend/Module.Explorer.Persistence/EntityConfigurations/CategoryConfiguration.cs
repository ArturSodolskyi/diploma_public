using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Explorer.Domain;

namespace Module.Explorer.Persistence.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Parent)
               .WithMany(x => x.Categories)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
