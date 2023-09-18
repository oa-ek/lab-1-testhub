using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders
{
    public class QuestionSeeder
    {
        private readonly TestHubDbContext _context;

        public QuestionSeeder(TestHubDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Set<Question>().Any()) return;

            var questions = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    Title = "Question 1",
                    Description = "Description for Question 1",
                    Image = "image1.jpg",
                    TestId = 1,
                    TypeId = 1
                },
                new Question
                {
                    Id = 2,
                    Title = "Question 2",
                    Description = "Description for Question 2",
                    Image = "image2.jpg",
                    TestId = 1,
                    TypeId = 2
                },
                new Question
                {
                    Id = 3,
                    Title = "Question 3",
                    Description = "Description for Question 3",
                    Image = "image3.jpg",
                    TestId = 2,
                    TypeId = 3
                },
                new Question
                {
                    Id = 4,
                    Title = "Question 4",
                    Description = "Description for Question 4",
                    Image = "image4.jpg",
                    TestId = 2,
                    TypeId = 4
                },
                new Question
                {
                    Id = 5,
                    Title = "Question 5",
                    Description = "Description for Question 5",
                    Image = "image5.jpg",
                    TestId = 3,
                    TypeId = 5
                }
            };

            _context.Set<Question>().AddRange(questions);
            _context.SaveChanges();
        }
    }
}
