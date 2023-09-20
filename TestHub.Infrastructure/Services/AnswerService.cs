using TestHub.Core.Dtos;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Services;

public class AnswerService
{
    public Answer GetAswer(AnswerDto answerDto)
    {
        return new Answer
        {
            Text = answerDto.Text,
            Image = answerDto.Image,
            IsCorrect = answerDto.IsCorrect,
            IsStrictText = answerDto.IsStrictText,
            QuestionId = 1
        };
    }
}