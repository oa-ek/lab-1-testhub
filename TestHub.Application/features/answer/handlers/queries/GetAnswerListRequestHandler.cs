using Application.contracts.persistence;
using Application.dtos.respondsDto;
using Application.features.answer.requests.queries;

namespace Application.features.answer.handlers.queries;

public class GetAnswerListRequestHandler : IRequestHandler<GetAnswerListRequest, List<RespondAnswerDto>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerListRequestHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<RespondAnswerDto>> Handle(GetAnswerListRequest request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAll();

        return _mapper.Map<List<RespondAnswerDto>>(categories);
    }
}