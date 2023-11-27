using Application.features.answer.requests.queries;

namespace Application.features.answer.handlers.queries;

public class GetAnswerListRequestHandler : IRequestHandler<GetAnswerListRequest, BaseCommandResponse<List<RespondAnswerDto>>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerListRequestHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<List<RespondAnswerDto>>> Handle(GetAnswerListRequest request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAll();

        var respondAnswerDtos = _mapper.Map<List<RespondAnswerDto>>(categories);
        return new OkSuccessStatusResponse<List<RespondAnswerDto>>(respondAnswerDtos);
    }
}