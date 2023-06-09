using Core.Repository.Entities;
using System.Linq.Expressions;

namespace Core.Repository.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = true, string includes = null);
    Task<T> GetById(string id);
    Task<T> AddAsync(T obj);
    Task<IReadOnlyList<T>> AddRangeAsync(IEnumerable<T> obj);
    Task<IReadOnlyList<T>> UpdateRangeAsync(IEnumerable<T> obj);
    Task<T> UpdateAsync(T obj);
    Task<bool> DeleteAsync(T obj);
    Task<bool> DeleteRangeAsync(IEnumerable<T> obj);

    Task<IReadOnlyList<T>> GetQuery(Expression<Func<T, bool>> predicate);
}