using Application.features.question.requests.queries;
using Application.results.common;

namespace Application.features.question.handlers.queries;

public class GetQuestionDetailedListRequestByTestHandler : IRequestHandler<GetQuestionDetailedListRequestByTest, BaseCommandResult<List<RespondQuestionDto>>>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public GetQuestionDetailedListRequestByTestHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    
    public async Task<BaseCommandResult<List<RespondQuestionDto>>> Handle(GetQuestionDetailedListRequestByTest request, CancellationToken cancellationToken)
    {
        var questions = await _repository.GetQuestionsWithDetailsByTest(request.TestId);

        var respondQuestionsDtos = _mapper.Map<List<RespondQuestionDto>>(questions);
        return new OkSuccessStatusResult<List<RespondQuestionDto>>(respondQuestionsDtos);
    }
}