using TestHub.Core.Context;
using TestHub.Core.Models;

namespace TestHub.Core.Seeders;

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
                Id = 1,
                Text = "Option 1",
                Image = "image1.jpg",
                IsCorrect = true,
                QuestionId = 1,
                IsStrictText = false
            },
            new Answer
            {
                Id = 2,
                Text = "Option 2",
                Image = "image2.jpg",
                IsCorrect = false,
                QuestionId = 1,
                IsStrictText = false
            },
            new Answer
            {
                Id = 3, 
                Text = "Answer A",
                Image = null,
                IsCorrect = true,
                QuestionId = 2,
                IsStrictText = true
            },
            new Answer
            {
                Id = 4, 
                Text = "Answer B",
                Image = null,
                IsCorrect = false,
                QuestionId = 2,
                IsStrictText = true
            },
            new Answer
            {
                Id = 5,
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