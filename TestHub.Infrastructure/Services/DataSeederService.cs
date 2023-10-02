using TestHub.Infrastructure.Context;
using TestHub.Infrastructure.Seeders;

namespace TestHub.Infrastructure.Services;

public class DataSeederService
{
    public class DataSeeder
    {
        private readonly TestHubDbContext _context;

        public DataSeeder(TestHubDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            // Виклики сідерів для ініціалізації даних
            var userSeeder = new UserSeeder(_context);
            userSeeder.Seed();
            
            var testSeeder = new TestSeeder(_context);
            testSeeder.Seed();
            
            var categorySeeder = new CategorySeeder(_context);
            categorySeeder.Seed();
            
            var testCategorySeeder = new TestCategorySeeder(_context);
            testCategorySeeder.Seed();
            
            var questionTypeSeeder = new QuestionTypeSeeder(_context);
            questionTypeSeeder.Seed();
            
            var questionSeeder = new QuestionSeeder(_context);
            questionSeeder.Seed();
            
            var answerSeeder = new AnswerSeeder(_context);
            answerSeeder.Seed();

            var testMetadataSeeder = new TestMetadataSeeder(_context);
            testMetadataSeeder.Seed();
            
            var testSessionSeeder = new TestSessionSeeder(_context);
            testSessionSeeder.Seed();

            var statusSessionQuestionSeeder = new StatusSessionQuestionSeeder(_context);
            statusSessionQuestionSeeder.Seed();
        }
    }
}