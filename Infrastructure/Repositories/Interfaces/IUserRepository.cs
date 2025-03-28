namespace Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<double> GetAverageCourierRating();
}