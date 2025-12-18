using Main.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Main.Infrastructure.EntityValidator
{
    public class TeacherValidator : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teachers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            //builder.HasOne<Branch>()
            //       .WithMany(b => b.Tables)
            //       .HasForeignKey(x => x.BranchId)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
