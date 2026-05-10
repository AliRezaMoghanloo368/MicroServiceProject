using Identity.Domain.Core.AggregateModels.UserItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Domain.Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users", "MGH");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value,
                x => new UserId(x)
            );
            builder.Property(x => x.Password).HasMaxLength(200).IsRequired();

            builder.OwnsOne(x => x.UserInfo, a =>
            {
                a.Property(x => x.FullName).HasMaxLength(200).HasColumnName("FullName").IsRequired();
                a.Property(x => x.Email).HasMaxLength(200).HasColumnName("Email");
                a.Property(x => x.PhoneNumber).HasMaxLength(20).HasColumnName("PhoneNumber").IsRequired();
            });
        }
    }
}
