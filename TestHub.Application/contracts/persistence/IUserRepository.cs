namespace Application.contracts.persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmail(string email);
    Task AddResetPassword(User user, string password);
    Task<User> SetRefreshToken(User user, string refreshToken);
    Task VerifiedEmail(User user);
}