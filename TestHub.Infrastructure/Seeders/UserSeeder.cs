using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders
{
    public class UserSeeder
    {
        private readonly TestHubDbContext _context;

        public UserSeeder(TestHubDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Set<User>().Any()) return;

            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "user1@example.com",
                    Password = "password1",
                    Name = "User 1",
                    Role = "User",
                    CreateAt = DateTime.Now,
                    UpdateAd = DateTime.Now,
                    DeleteAt = DateTime.Now,
                    Comment = "This is User 1"
                },
                new User
                {
                    Id = 2,
                    Email = "user2@example.com",
                    Password = "password2",
                    Name = "User 2",
                    Role = "User",
                    CreateAt = DateTime.Now,
                    UpdateAd = DateTime.Now,
                    DeleteAt = DateTime.Now,
                    Comment = "This is User 2"
                },
                new User
                {
                    Id = 3,
                    Email = "user3@example.com",
                    Password = "password3",
                    Name = "User 3",
                    Role = "User",
                    CreateAt = DateTime.Now,
                    UpdateAd = DateTime.Now,
                    DeleteAt = DateTime.Now,
                    Comment = "This is User 3"
                },
                new User
                {
                    Id = 4,
                    Email = "user4@example.com",
                    Password = "password4",
                    Name = "User 4",
                    Role = "User",
                    CreateAt = DateTime.Now,
                    UpdateAd = DateTime.Now,
                    DeleteAt = DateTime.Now,
                    Comment = "This is User 4"
                },
                new User
                {
                    Id = 5,
                    Email = "user5@example.com",
                    Password = "password5",
                    Name = "User 5",
                    Role = "User",
                    CreateAt = DateTime.Now,
                    UpdateAd = DateTime.Now,
                    DeleteAt = DateTime.Now,
                    Comment = "This is User 5"
                }
            };

            _context.Set<User>().AddRange(users);
            _context.SaveChanges();
        }
    }
}
