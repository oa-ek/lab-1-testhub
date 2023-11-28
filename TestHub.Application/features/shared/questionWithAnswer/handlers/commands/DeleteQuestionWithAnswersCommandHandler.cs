using Application.features.shared.questionwithanswer.requests.commands;

namespace Application.features.shared.questionwithanswer.handlers.commands;

public class DeleteQuestionWithAnswersCommandHandler : IRequestHandler<DeleteQuestionWithAnswersCommand, BaseCommandResponse<RespondQuestionDto>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;

    public DeleteQuestionWithAnswersCommandHandler(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
    {
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
    }
    
    public async Task<BaseCommandResponse<RespondQuestionDto>> Handle(DeleteQuestionWithAnswersCommand command, CancellationToken cancellationToken)
    {
        var getAnswersRequest = await _answerRepository.GetByQuestion(command.QuestionId);
        if (getAnswersRequest.Any())
            await _answerRepository.DeleteRange(getAnswersRequest);
        
        var question = await _questionRepository.Get(command.QuestionId);
        if (question == null) return new NotFoundFailedStatusResponse<RespondQuestionDto>(command.QuestionId);
        await _questionRepository.Delete(question);
        
        return new OkSuccessStatusResponse<RespondQuestionDto>(command.QuestionId);
    }
}