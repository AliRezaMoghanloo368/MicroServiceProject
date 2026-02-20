using Identity.Domain.Core.AggregateModels.Users;
using Identity.Domain.Core.Common.SeedWork.Interfaces;
using Identity.Domain.Infra.Data.Configurations;

namespace Identity.Domain.Infra.Data.Context
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}