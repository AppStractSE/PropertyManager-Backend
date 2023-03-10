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
        _context.SavingChanges += Context_SavingChanges;
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
        var idProperty = entity.GetType().GetProperty("Id").GetValue(entity, null);
        var oldEntity = await _context.Set<T>().FindAsync(idProperty);
        _context.ChangeTracker.Clear();

        SetRowData(entity, oldEntity);
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IReadOnlyList<T>> UpdateRangeAsync(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
        await _context.SaveChangesAsync();
        return entities.ToList();
    }
    public async Task<IReadOnlyList<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
        return entities.ToList();
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    private void Context_SavingChanges(object sender, SavingChangesEventArgs args)
    {
        var entities = _context.ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && x.Entity is T &&
            (x.State == EntityState.Added || x.State == EntityState.Modified))
            .ToArray();

        foreach (var entity in entities)
        {
            var baseEntity = entity.Entity as BaseEntity;

            if (entity.State == EntityState.Added)
            {
                baseEntity.RowCreated = DateTime.Now;
            }

            baseEntity.RowModified = DateTime.Now;
            baseEntity.RowVersion += 1;
        }
    }

    private void SetRowData(T entity, T oldEntity)
    {
        entity.RowCreated = oldEntity.RowCreated;
        entity.RowModified = oldEntity.RowModified;
        entity.RowVersion = oldEntity.RowVersion;
    }
}
