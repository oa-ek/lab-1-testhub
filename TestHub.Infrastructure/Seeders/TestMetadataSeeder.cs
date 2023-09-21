using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders
{
    public class TestMetadataSeeder
    {
        private readonly TestHubDbContext _context;

        public TestMetadataSeeder(TestHubDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Set<TestMetadata>().Any()) return;

            var testMetadata = new List<TestMetadata>
            {
                new TestMetadata
                {
                    UserId = 1,
                    TestId = 1,
                    Like = 1,
                    Rating = 5
                },
                new TestMetadata
                {
                    UserId = 2,
                    TestId = 1,
                    Like = 0,
                    Rating = 4
                },
                new TestMetadata
                {
                    UserId = 1,
                    TestId = 2,
                    Like = 1,
                    Rating = 3
                },
                new TestMetadata
                {
                    UserId = 3,
                    TestId = 3,
                    Like = 1,
                    Rating = 5
                },
                new TestMetadata
                {
                    UserId = 2,
                    TestId = 4,
                    Like = 0,
                    Rating = 2
                }
            };

            _context.Set<TestMetadata>().AddRange(testMetadata);
            _context.SaveChanges();
        }
    }
}