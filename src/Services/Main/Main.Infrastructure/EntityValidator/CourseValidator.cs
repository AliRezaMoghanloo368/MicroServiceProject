using Main.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Main.Infrastructure.EntityValidator
{
    public class CourseValidator : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            //builder.HasOne<Branch>()
            //       .WithMany(b => b.Tables)
            //       .HasForeignKey(x => x.BranchId)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
