using Application.dtos;
using Application.features.test.requests.queries;
using Application.persistence.contracts;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedListRequestHandler : IRequestHandler<GetTestDetailedListRequest, List<TestDto>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedListRequestHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<TestDto>> Handle(GetTestDetailedListRequest request, CancellationToken cancellationToken)
    {
        var tests = await _repository.GetTestWithDetails();

        return _mapper.Map<List<TestDto>>(tests);
    }
}