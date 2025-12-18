using Main.Domain.Common;
using Main.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure.Persistence
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }

        #region SaveChangesAsync
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    foreach (var entry in ChangeTracker.Entries<EntityBase>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreateDate = DateTime.Now;
        //                entry.Entity.CreatedBy = "mohammad";
        //                break;
        //            case EntityState.Modified:
        //                entry.Entity.ModifiedDate = DateTime.Now;
        //                entry.Entity.LastModifiedBy = "mohammad";
        //                break;
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}
        #endregion
    }
}
