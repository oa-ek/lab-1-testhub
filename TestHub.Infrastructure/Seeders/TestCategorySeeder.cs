﻿using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders
{
    public class TestCategorySeeder
    {
        private readonly TestHubDbContext _context;

        public TestCategorySeeder(TestHubDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Set<TestCategory>().Any()) return;

            var testCategories = new List<TestCategory>
            {
                new TestCategory
                {
                    TestId = 1,
                    CategoryId = 1 
                },
                new TestCategory
                {
                    TestId = 2,  
                    CategoryId = 2 
                },
                new TestCategory
                {
                    TestId = 3,  
                    CategoryId = 3 
                },
                new TestCategory
                {
                    TestId = 4, 
                    CategoryId = 4  
                },
                new TestCategory
                {
                    TestId = 5,
                    CategoryId = 5 
                }
            };

            _context.Set<TestCategory>().AddRange(testCategories);
            _context.SaveChanges();
        }
    }
}