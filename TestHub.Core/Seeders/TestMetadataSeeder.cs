using TestHub.Core.Context;
using TestHub.Core.Models;

namespace TestHub.Core.Seeders
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
                    Id = 1,
                    UserId = 1,
                    TestId = 1,
                    Like = 1,
                    Rating = 5
                },
                new TestMetadata
                {
                    Id = 2,
                    UserId = 2,
                    TestId = 1,
                    Like = 0,
                    Rating = 4
                },
                new TestMetadata
                {
                    Id = 3,
                    UserId = 1,
                    TestId = 2,
                    Like = 1,
                    Rating = 3
                },
                new TestMetadata
                {
                    Id = 4,
                    UserId = 3,
                    TestId = 3,
                    Like = 1,
                    Rating = 5
                },
                new TestMetadata
                {
                    Id = 5,
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