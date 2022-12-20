using System.Collections.Generic;
using Domain.Repository.Entities;

namespace Domain.Repository.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = true);
}