using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders
{
    public class StatusSessionQuestionSeeder
    {
        private readonly TestHubDbContext _context;

        public StatusSessionQuestionSeeder(TestHubDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Set<StatusSessionQuestion>().Any()) return;

            var statusSessionQuestions = new List<StatusSessionQuestion>
            {
                new StatusSessionQuestion
                {
                    SessionId = 1,
                    QuestionId = 1,
                    IsCorrect = true,
                    Attepts = 1
                },
                new StatusSessionQuestion
                {
                    SessionId = 2,
                    QuestionId = 2,
                    IsCorrect = true,
                    Attepts = 2
                },
                new StatusSessionQuestion
                {
                    SessionId = 3,
                    QuestionId = 3,
                    IsCorrect = false,
                    Attepts = 3
                },
                new StatusSessionQuestion
                {
                    SessionId = 4,
                    QuestionId = 1,
                    IsCorrect = true,
                    Attepts = 1
                },
                new StatusSessionQuestion
                {
                    SessionId = 5,
                    QuestionId = 4,
                    IsCorrect = false,
                    Attepts = 2
                }
            };

            _context.Set<StatusSessionQuestion>().AddRange(statusSessionQuestions);
            _context.SaveChanges();
        }
    }
}