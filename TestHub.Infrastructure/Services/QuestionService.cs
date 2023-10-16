using System.Linq.Expressions;
using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class QuestionService
{
    private readonly GenericRepository<Question> _questionRepository;
    private readonly GenericRepository<QuestionType> _questionTypeRepository;

    public QuestionService(GenericRepository<Question> questionRepository, 
        GenericRepository<QuestionType> questionTypeRepository)
    {
        _questionRepository = questionRepository;
        _questionTypeRepository = questionTypeRepository;
    }
    
    public IEnumerable<Question> GetAll()
    {
        return _questionRepository.Get();
    }

    public IEnumerable<Question> GetAllByTest(int testId)
    {
        Expression<Func<Question, bool>> filter = q =>q.Test.Id== testId;
        return _questionRepository.Get(filter, null, "Test,Answers");
    }

    public Question GetById(int id)
    {
        return _questionRepository.GetByID(id);
    }

    public void Add(Question question)
    {
        _questionRepository.Insert(question);
    }

    public void Delete(Question questionToDelete)
    {
        _questionRepository.Delete(questionToDelete);
    }

    public void Update(Question questionToUpdate, QuestionDto questionChanging)
    {
        questionToUpdate.Title = questionChanging.Title;
        questionToUpdate.Description = questionChanging.Description;
        questionToUpdate.Image = questionChanging.Image;
        
        _questionRepository.Update(questionToUpdate);
    }

    public IEnumerable<QuestionType> GetQuestionTypes()
    {
        return _questionTypeRepository.Get();
    }
}