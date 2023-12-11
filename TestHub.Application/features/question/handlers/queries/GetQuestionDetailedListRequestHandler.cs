using Application.features.question.requests.queries;
using Application.results.common;

namespace Application.features.question.handlers.queries;

public class GetQuestionDetailedListRequestHandler : IRequestHandler<GetQuestionDetailedListRequest, BaseCommandResult<List<RespondQuestionDto>>>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public GetQuestionDetailedListRequestHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResult<List<RespondQuestionDto>>> Handle(GetQuestionDetailedListRequest request, CancellationToken cancellationToken)
    {
        var questions = await _repository.GetQuestionsWithDetails();
        
        var respondQuestionDtos = _mapper.Map<List<RespondQuestionDto>>(questions);
        return new OkSuccessStatusResult<List<RespondQuestionDto>>(respondQuestionDtos);
    }
}