using Domain.Repository.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    public DbContext _context;
    public BaseRepository(DbContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = true)
    {
        IQueryable<T> source = _context.Set<T>();
        return await source.ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        var result = await _context.Set<T>().FindAsync(id);
        if(result == null) {
            throw new Exception("Id not found");
        }
        return result;
    }
}