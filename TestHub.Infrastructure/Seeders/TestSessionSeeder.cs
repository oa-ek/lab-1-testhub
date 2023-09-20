using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders
{
    public class TestSessionSeeder
    {
        private readonly TestHubDbContext _context;

        public TestSessionSeeder(TestHubDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Set<TestSession>().Any()) return;

            var testSessions = new List<TestSession>
            {
                new TestSession
                {
                    StartedAt = DateTime.UtcNow.AddHours(-2),
                    FinishedAt = DateTime.UtcNow.AddHours(-1),
                    UserId = 1,
                    TestId = 1,
                    Result = 80,
                    IsTraining = 0
                },
                new TestSession
                {
                    StartedAt = DateTime.UtcNow.AddHours(-3),
                    FinishedAt = DateTime.UtcNow.AddHours(-2),
                    UserId = 2,
                    TestId = 2,
                    Result = 95,
                    IsTraining = 0
                },
                new TestSession
                {
                    StartedAt = DateTime.UtcNow.AddHours(-4),
                    FinishedAt = DateTime.UtcNow.AddHours(-3),
                    UserId = 1,
                    TestId = 3,
                    Result = 75,
                    IsTraining = 0
                },
                new TestSession
                {
                    StartedAt = DateTime.UtcNow.AddHours(-5),
                    FinishedAt = DateTime.UtcNow.AddHours(-4),
                    UserId = 3,
                    TestId = 1,
                    Result = 90,
                    IsTraining = 0
                },
                new TestSession
                {
                    StartedAt = DateTime.UtcNow.AddHours(-6),
                    FinishedAt = DateTime.UtcNow.AddHours(-5),
                    UserId = 2,
                    TestId = 4,
                    Result = 60,
                    IsTraining = 0
                }
            };

            _context.Set<TestSession>().AddRange(testSessions);
            _context.SaveChanges();
        }
    }
}
