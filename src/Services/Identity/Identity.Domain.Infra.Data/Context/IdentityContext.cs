using Identity.Domain.Core.AggregateModels.UserItems;
using Identity.Domain.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Identity.Domain.Infra.Data.Context
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}