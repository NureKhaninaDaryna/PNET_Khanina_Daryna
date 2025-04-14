using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Repositories;
 
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly DbContext _context;

    protected readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Id.Equals(id));
    }

    public async Task<List<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task CreateAsync(T item)
    {
        await _dbSet.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CreateAndReturnIdAsync(T item)
    {
       var result = await _dbSet.AddAsync(item);
       await _context.SaveChangesAsync();
       
       return result.Entity.Id;
    }

    public async Task UpdateAsync(T item)
    {
        if (item is not BaseEntity entityWithId)
            throw new InvalidOperationException("Entity must implement BaseEntity.");

        var entry = _context.ChangeTracker.Entries<T>()
            .FirstOrDefault(e => e.Entity is BaseEntity trackedEntity && trackedEntity.Id == entityWithId.Id);

        if (entry != null)
            entry.State = EntityState.Detached;

        foreach (var navigation in _context.Entry(item).Navigations)
        {
            if (navigation.Metadata is INavigation nav && nav.IsCollection == false)
            {
                navigation.CurrentValue = null;
            }
        }

        _dbSet.Update(item);

        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveByIdAsync(int id)
    {
        var itemToRemove = await GetByIdAsync(id);

        if (itemToRemove != null)
        {
            _context.Remove(itemToRemove);
        }

        await _context.SaveChangesAsync();
    }

    public List<T> GetAll()  => _dbSet.ToList();
    
    public List<T> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        return _dbSet
            .Where(predicate)
            .ToList();
    }

    public Task<int> GetLastIdAsync()
    {
        throw new NotImplementedException();
    }
}