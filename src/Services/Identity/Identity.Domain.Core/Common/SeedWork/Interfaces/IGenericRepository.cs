using System.Linq.Expressions;

namespace Identity.Domain.Core.Common.SeedWork.Interfaces
{
    public interface IGenericRepository<T> where T : class, IAggregateRoot
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);

        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default!);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default!);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default!);
    }
}
