namespace Application.contracts.persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmail(string email);
    Task AddResetPassword(User user, string password);
    Task VerifiedEmail(User user);
}