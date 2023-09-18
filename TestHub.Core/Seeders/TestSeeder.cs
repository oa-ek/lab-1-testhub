using TestHub.Core.Context;
using TestHub.Core.Enum;
using TestHub.Core.Models;

namespace TestHub.Core.Seeders
{
    public class TestSeeder
    {
        private readonly TestHubDbContext _context;

        public TestSeeder(TestHubDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Set<Test>().Any()) return;

            var tests = new List<Test>
            {
                new Test
                {
                    Id = 1,
                    Title = "Test 1",
                    Description = "Description for Test 1",
                    Duration = 60,
                    OwnerId = 1,
                    Status = TestStatus.Active,
                    IsPublic = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Test
                {
                    Id = 2,
                    Title = "Test 2",
                    Description = "Description for Test 2",
                    Duration = 45,
                    OwnerId = 2,
                    Status = TestStatus.Archived,
                    IsPublic = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Test
                {
                    Id = 3,
                    Title = "Test 3",
                    Description = "Description for Test 3",
                    Duration = 30,
                    OwnerId = 1,
                    Status = TestStatus.Active,
                    IsPublic = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Test
                {
                    Id = 4,
                    Title = "Test 4",
                    Description = "Description for Test 4",
                    Duration = 75,
                    OwnerId = 3,
                    Status = TestStatus.Draft,
                    IsPublic = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Test
                {
                    Id = 5,
                    Title = "Test 5",
                    Description = "Description for Test 5",
                    Duration = 90,
                    OwnerId = 2,
                    Status = TestStatus.Archived,
                    IsPublic = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                }
            };

            _context.Set<Test>().AddRange(tests);
            _context.SaveChanges();
        }
    }
}
