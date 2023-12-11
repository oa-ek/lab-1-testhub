using Application.features.shared.questionwithanswer.requests.commands;
using Application.results.common;

namespace Application.features.shared.questionwithanswer.handlers.commands;

public class DeleteQuestionWithAnswersCommandHandler : IRequestHandler<DeleteQuestionWithAnswersCommand, BaseCommandResult<RespondQuestionDto>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;

    public DeleteQuestionWithAnswersCommandHandler(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
    {
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
    }
    
    public async Task<BaseCommandResult<RespondQuestionDto>> Handle(DeleteQuestionWithAnswersCommand command, CancellationToken cancellationToken)
    {
        var getAnswersRequest = await _answerRepository.GetByQuestion(command.QuestionId);
        if (getAnswersRequest.Any())
            await _answerRepository.DeleteRange(getAnswersRequest);
        
        var question = await _questionRepository.Get(command.QuestionId);
        if (question == null) return new NotFoundFailedStatusResult<RespondQuestionDto>(command.QuestionId);
        await _questionRepository.Delete(question);
        
        return new OkSuccessStatusResult<RespondQuestionDto>(command.QuestionId);
    }
}