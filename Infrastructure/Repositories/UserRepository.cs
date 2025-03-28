using Domain.Enums;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class UserRepository(IMongoDatabase database)
    : MongoRepository<User>(database, "Users"), IUserRepository
{
    public async Task<double> GetAverageCourierRating()
    {
        var result = await Collection.Aggregate()
            .Match(user => user.Role == UserRole.Courier)
            .Group(new BsonDocument { { "_id", "$Role" }, { "AverageRating", new BsonDocument("$avg", "$Rating") } })
            .FirstOrDefaultAsync();

        var rating = result["AverageRating"];
        
        return rating != null ? rating.AsDouble : 0;
    }
}