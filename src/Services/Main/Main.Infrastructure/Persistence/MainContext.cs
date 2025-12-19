using Main.Domain.Models;
using Main.Infrastructure.EntityValidator;
using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure.Persistence
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Fluent API Configurations
            modelBuilder.ApplyConfiguration(new StudentValidator());
            modelBuilder.ApplyConfiguration(new TeacherValidator());
            modelBuilder.ApplyConfiguration(new CourseValidator());
            base.OnModelCreating(modelBuilder);
        }

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
