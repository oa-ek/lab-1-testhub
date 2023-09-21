using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class AnswerService
{
    private readonly GenericRepository<Answer> _answerRepository;

    public AnswerService(GenericRepository<Answer> answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public Answer GetAswer(AnswerDto answerDto, int questionId)
    {
        return new Answer
        {
            Text = answerDto.Text,
            Image = answerDto.Image,
            IsCorrect = answerDto.IsCorrect,
            IsStrictText = answerDto.IsStrictText,
            QuestionId = questionId
        };
    }
    
    public IEnumerable<Answer> GetAll()
    {
        return _answerRepository.Get();
    }

    public Answer GetById(int id)
    {
        return _answerRepository.GetByID(id);
    }
    

    public void Delete(Answer answerToDelete)
    {
        _answerRepository.Delete(answerToDelete);
    }

    public void Update(Answer answerToUpdate, AnswerDto answerChanging)
    {
        answerToUpdate.Text = answerChanging.Text;
        answerToUpdate.Image = answerChanging.Image;
        answerToUpdate.IsCorrect = answerChanging.IsCorrect;
        answerToUpdate.IsStrictText = answerChanging.IsStrictText;
        
        _answerRepository.Update(answerToUpdate);
    }
}