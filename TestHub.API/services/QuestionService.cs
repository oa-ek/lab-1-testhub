using Application.dtos.respondsDto;
using Application.features.answer.requests.commands;
using Application.features.answer.requests.queries;
using Application.features.question.requests.commands;
using Application.responses.success;
using AutoMapper;

namespace TestHub.API.services;

public class QuestionService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public QuestionService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<RespondQuestionDto>> CreateQuestion(int testId,
        RequestQuestionWithAnswerDto? requestQuestionWithAnswerDto)
    {
        var questionDto = _mapper.Map<RequestQuestionDto>(requestQuestionWithAnswerDto);
        var createQuestionCommand = new CreateQuestionCommand
        {
            QuestionDto = questionDto,
            TestId = testId
        };

        return await _mediator.Send(createQuestionCommand);
    }

    public async Task<BaseCommandResponse<RespondAnswerDto>> CreateAnswers(int questionId,
        RequestAnswerDto[]? answerDtos)
    {
        foreach (var answerDto in answerDtos!)
        {
            var createAnswerCommand = new CreateAnswerCommand
            {
                AnswerDto = answerDto,
                QuestionId = questionId
            };

            var createAnswerResponse = await _mediator.Send(createAnswerCommand);

            if (!createAnswerResponse.Success)
                return createAnswerResponse;
        }

        return new CreatedSuccessStatusResponse<RespondAnswerDto>(questionId);
    }

    public async Task<BaseCommandResponse<RespondQuestionDto>> UpdateQuestion(int questionId,
        RequestQuestionWithAnswerDto? requestQuestionWithAnswerDto)
    {
        var questionDto = _mapper.Map<RequestQuestionDto>(requestQuestionWithAnswerDto);
        var updateQuestionCommand = new UpdateQuestionCommand
        {
            QuestionDto = questionDto,
            Id = questionId
        };

        return await _mediator.Send(updateQuestionCommand);
    }

    public async Task<BaseCommandResponse<RespondAnswerDto>> UpdateAnswers(int questionId,
        RequestAnswerDto[]? answerDtos)
    {
        await DeleteAnswers(questionId);
        var response = await CreateAnswers(questionId, answerDtos);
        
        return response;
    }
    
    public async Task<BaseCommandResponse<RespondQuestionDto>> DeleteQuestion(int questionId)
    {
        var deleteQuestionCommand = new DeleteQuestionCommand { Id = questionId };
        return await _mediator.Send(deleteQuestionCommand);
    }

    public async Task<BaseCommandResponse<RespondAnswerDto>> DeleteAnswers(int questionId)
    {
        var getAnswerListRequest = new GetAnswerListRequestByQuestion { QuestionId = questionId };
        var answers = await _mediator.Send(getAnswerListRequest);

        foreach (var answer in answers)
        {
            var deleteAnswerCommand = new DeleteAnswerCommand { Id = answer.Id };
            var deleteAnswerResponse = await _mediator.Send(deleteAnswerCommand);

            if (!deleteAnswerResponse.Success)
                return deleteAnswerResponse;
        }

        return new OkSuccessStatusResponse<RespondAnswerDto>(questionId);
    }
}