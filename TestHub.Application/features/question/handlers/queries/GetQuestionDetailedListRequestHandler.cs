using Application.features.question.requests.queries;

namespace Application.features.question.handlers.queries;

public class GetQuestionDetailedListRequestHandler : IRequestHandler<GetQuestionDetailedListRequest, BaseCommandResponse<List<RespondQuestionDto>>>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public GetQuestionDetailedListRequestHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<List<RespondQuestionDto>>> Handle(GetQuestionDetailedListRequest request, CancellationToken cancellationToken)
    {
        var questions = await _repository.GetQuestionsWithDetails();
        
        var respondQuestionDtos = _mapper.Map<List<RespondQuestionDto>>(questions);
        return new OkSuccessStatusResponse<List<RespondQuestionDto>>(respondQuestionDtos);
    }
}