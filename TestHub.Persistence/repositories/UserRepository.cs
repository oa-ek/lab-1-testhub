using Application.contracts.persistence;

namespace TestHub.Persistence.repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly TestHubDbContext _context;

    public UserRepository(TestHubDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var user = await _context.Users!
            .FirstOrDefaultAsync(q => q.Email == email);

        return user;
    }

    public async Task AddResetPassword(User user, string password)
    {
        user.Password = password;
        _context.Users!.Update(user);

        await _context.SaveChangesAsync();
    }

    public async Task<User> SetRefreshToken(User user, string refreshToken)
    {
        user.Token = refreshToken;
        _context.Users!.Update(user);

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task VerifiedEmail(User user)
    {
        user.IsEmailVerified = true;
        _context.Users!.Update(user);

        await _context.SaveChangesAsync();
    }
}