using Main.Domain.Models;

namespace Main.Application.Contracts.Persistence
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task InActiveAsync(int id);
    }
}
