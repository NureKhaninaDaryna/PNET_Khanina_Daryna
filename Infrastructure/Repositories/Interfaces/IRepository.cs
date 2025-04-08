using System.Linq.Expressions;
using Domain.Models;

namespace Infrastructure.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    public Task<T?> GetByIdAsync(int id);

    public Task<List<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate);

    public Task UpdateAsync(T item);

    public Task<List<T>> GetAllAsync();

    public Task CreateAsync(T item);
    
    Task<int> CreateAndReturnIdAsync(T item);

    public Task RemoveByIdAsync(int id);
    
    public List<T> GetAll();
    
    public List<T> GetByPredicate(Expression<Func<T, bool>> predicate);

    public Task<int> GetLastIdAsync();
}
