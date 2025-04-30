namespace FarmToFork.Core.Repositories.Base;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(bool disableTracking = true);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}

