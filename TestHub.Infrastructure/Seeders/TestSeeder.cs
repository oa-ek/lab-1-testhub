using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders
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
                    Title = "Test 1",
                    Description = "Description for Test 1",
                    Duration = 60,
                    OwnerId = 1,
                    Status = "Active",
                    IsPublic = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Test
                {
                    Title = "Test 2",
                    Description = "Description for Test 2",
                    Duration = 45,
                    OwnerId = 2,
                    Status = "Inactive",
                    IsPublic = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Test
                {
                    Title = "Test 3",
                    Description = "Description for Test 3",
                    Duration = 30,
                    OwnerId = 1,
                    Status = "Active",
                    IsPublic = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Test
                {
                    Title = "Test 4",
                    Description = "Description for Test 4",
                    Duration = 75,
                    OwnerId = 3,
                    Status = "Active",
                    IsPublic = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Test
                {
                    Title = "Test 5",
                    Description = "Description for Test 5",
                    Duration = 90,
                    OwnerId = 2,
                    Status = "Inactive",
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
