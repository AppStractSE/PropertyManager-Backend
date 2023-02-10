using Domain.Repository.Entities;
using Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    public DbContext _context;
    public BaseRepository(DbContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = true, string includes = null)
    {
        IQueryable<T> source = _context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                source = source.Include(include);
            }
        }
        return await source.ToListAsync();
    }

    public async Task<T> GetById(string id)
    {
        var result = await _context.Set<T>().FindAsync(Guid.Parse(id));
        if (result == null)
        {
            throw new Exception("Id not found");
        }
        return result;
    }

    public async Task<IReadOnlyList<T>> GetQuery(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}