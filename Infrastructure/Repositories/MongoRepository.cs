using MongoDB.Driver;
using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

public class MongoRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly IMongoCollection<T> Collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        Collection = database.GetCollection<T>(collectionName);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
    {
        return await Collection.Find(predicate).ToListAsync();
    }

    public async Task UpdateAsync(T item)
    {
        await Collection.ReplaceOneAsync(x => x.Id == item.Id, item);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task CreateAsync(T item)
    {
        item.Id = await GetLastIdAsync() + 1;
        
        await Collection.InsertOneAsync(item);
    }

    public async Task RemoveByIdAsync(int id)
    {
        await Collection.DeleteOneAsync(x => x.Id == id);
    }

    public List<T> GetAll()
    {
        return Collection.Find(_ => true).ToList();
    }

    public List<T> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        return Collection.Find(predicate).ToList();
    }
    
    public async Task<int> GetLastIdAsync()
    {
        var lastDocument = await Collection.Find(FilterDefinition<T>.Empty)
            .Sort(Builders<T>.Sort.Descending("Id"))
            .Limit(1)
            .FirstOrDefaultAsync();

        return lastDocument?.Id ?? 0;
    }
}