using System.Linq.Expressions;
using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class QuestionService
{
    private readonly GenericRepository<Question> _questionRepository;
    private readonly GenericRepository<QuestionType> _questionTypeRepository;
    private readonly AnswerService _answerService;
    private readonly FileService _fileService;

    public QuestionService(GenericRepository<Question> questionRepository, 
        GenericRepository<QuestionType> questionTypeRepository, AnswerService answerService, FileService fileService)
    {
        _questionRepository = questionRepository;
        _questionTypeRepository = questionTypeRepository;
        _answerService = answerService;
        _fileService = fileService;
    }
    
    public IEnumerable<Question> GetAll()
    {
        return _questionRepository.Get();
    }

    public IEnumerable<Question> GetAllByTest(int testId)
    {
        Expression<Func<Question, bool>> filter = q =>q.Test.Id== testId;
        return _questionRepository.Get(filter, null, "Test,Answers,Type");
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

    public async Task Update(Question questionToUpdate, QuestionDto questionChanging, QuestionType type)
    {
        questionToUpdate.Title = questionChanging.Title;
        questionToUpdate.Description = questionChanging.Description;
        questionToUpdate.Image =
            questionChanging.q_image == null ? questionToUpdate.Image : await _fileService.UploadImage(questionChanging.q_image);
        questionToUpdate.Type = type;
        
        _questionRepository.Update(questionToUpdate);
    }

    public IEnumerable<QuestionType> GetQuestionTypes()
    {
        return _questionTypeRepository.Get();
    }

    public void DeleteAnswers(Question questionToDelete)
    {
        var answersToDelete = _answerService.GetAll().Where(a => a.QuestionId == questionToDelete.Id).ToList();
    
        // Видалити всі знайдені відповіді
        foreach (var answer in answersToDelete)
        {
            _answerService.Delete(answer);
        }
    }
}