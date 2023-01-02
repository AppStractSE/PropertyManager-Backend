using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Repository.Entities;

namespace Domain.Repository.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = true);
    Task<T> GetById(string id);
    Task<T> AddAsync(T obj);
    Task<IReadOnlyList<T>> GetQuery(Expression<Func<T, bool>> predicate);
}