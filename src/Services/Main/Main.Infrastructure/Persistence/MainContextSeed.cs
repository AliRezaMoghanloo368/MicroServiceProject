using Main.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Main.Infrastructure.Persistence
{
    public class MainContextSeed
    {
        public static async Task SeedAsync(MainContext context, ILogger<MainContextSeed> logger)
        {
            //if (!await context.Students.AnyAsync())
            //{
            //    //await context.Students.AddRangeAsync(GetPreconfiguredStudents());
            //    await context.SaveChangesAsync();
            //    logger.LogInformation("data seed section configured");
            //}
        }

        //public static IEnumerable<Student> GetPreconfiguredStudents()
        //{
        //    return null;
        //    return new List<Student>
        //    {
        //        new Student
        //        {
        //            FirstName = "xx",
        //            LastName = "xxx"
        //        }
        //    };
        //}
    }
}
