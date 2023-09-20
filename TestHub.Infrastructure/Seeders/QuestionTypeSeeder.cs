using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders
{
    public class QuestionTypeSeeder
    {
        private readonly TestHubDbContext _context;

        public QuestionTypeSeeder(TestHubDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Set<QuestionType>().Any()) return;

            var questionTypes = new List<QuestionType>
            {
                new QuestionType
                {
                    Type = "Single Choice Question"
                },
                new QuestionType
                {
                    Type = "Yes or No Question"
                },
                new QuestionType
                {
                    Type = "Matching Question"
                },
                new QuestionType
                {
                    Type = "Multiple Choice Question"
                },
                new QuestionType
                {
                    Type = "Fill in the Blanks Question"
                }
            };

            _context.Set<QuestionType>().AddRange(questionTypes);
            _context.SaveChanges();
        }
    }
}