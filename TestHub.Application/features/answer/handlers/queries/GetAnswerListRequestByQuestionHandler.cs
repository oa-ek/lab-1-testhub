using Application.features.answer.requests.queries;

namespace Application.features.answer.handlers.queries;

public class GetAnswerListRequestByQuestionHandler : IRequestHandler<GetAnswerListRequestByQuestion, List<Answer>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerListRequestByQuestionHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<Answer>> Handle(GetAnswerListRequestByQuestion request, CancellationToken cancellationToken)
    {
        var answers = await _repository.GetByQuestion(request.QuestionId);
        return answers.ToList();
    }
}