using Main.Domain.Models;

namespace Main.Application.Contracts.Persistence
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task InActiveAsync(int id);
    }
}
