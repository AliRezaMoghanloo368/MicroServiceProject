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

            builder.Property(x => x.CoverImageFileId)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            //Cascade
            //NoAction
            //Restrict: اگر معلم مربوطه حذف شد درس مربوط به آن حذف نشه و خطا بده
            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.StudentCourses)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
