using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders;

public class AnswerSeeder
{
    private readonly TestHubDbContext _context;

    public AnswerSeeder(TestHubDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Set<Answer>().Any()) return;
        
        var answers = new Answer[]
        {
            new Answer
            {
                Text = "Option 1",
                Image = "image1.jpg",
                IsCorrect = true,
                QuestionId = 1,
                IsStrictText = false
            },
            new Answer
            {
                Text = "Option 2",
                Image = "image2.jpg",
                IsCorrect = false,
                QuestionId = 1,
                IsStrictText = false
            },
            new Answer
            {
                Text = "Answer A",
                Image = null,
                IsCorrect = true,
                QuestionId = 2,
                IsStrictText = true
            },
            new Answer
            {
                Text = "Answer B",
                Image = null,
                IsCorrect = false,
                QuestionId = 2,
                IsStrictText = true
            },
            new Answer
            {
                Text = "Yes",
                Image = null,
                IsCorrect = true,
                QuestionId = 3,
                IsStrictText = false
            }
        };

        _context.Set<Answer>().AddRange(answers);
        _context.SaveChanges();
    }
}