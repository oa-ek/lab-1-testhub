using Application.dtos.requestsDto;
using Application.features.test.requests.queries;
using Application.persistence.contracts;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedListRequestHandler : IRequestHandler<GetTestDetailedListRequest, List<RequestTestDto>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedListRequestHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<RequestTestDto>> Handle(GetTestDetailedListRequest request, CancellationToken cancellationToken)
    {
        var tests = await _repository.GetTestWithDetails();

        return _mapper.Map<List<RequestTestDto>>(tests);
    }
}