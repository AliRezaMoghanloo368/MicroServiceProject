using Main.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Main.Infrastructure.EntityValidator
{
    public class StudentValidator : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.NationalCode)
                .IsRequired()
                .HasMaxLength(10);

            //builder.HasOne<Branch>()
            //       .WithMany(b => b.Tables)
            //       .HasForeignKey(x => x.BranchId)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
