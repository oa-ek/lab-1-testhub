using Application.features.answer.requests.queries;

namespace Application.features.answer.handlers.queries;

public class GetAnswerDtoListRequestHandler : IRequestHandler<GetAnswerDtoListRequest, BaseCommandResponse<List<RespondAnswerDto>>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerDtoListRequestHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<List<RespondAnswerDto>>> Handle(GetAnswerDtoListRequest request, CancellationToken cancellationToken)
    {
        var answers = await _repository.GetAll();

        var respondAnswerDtos = _mapper.Map<List<RespondAnswerDto>>(answers);
        return new OkSuccessStatusResponse<List<RespondAnswerDto>>(respondAnswerDtos);
    }
}