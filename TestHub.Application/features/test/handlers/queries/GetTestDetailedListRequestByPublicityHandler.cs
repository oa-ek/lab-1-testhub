using Application.features.test.requests.queries;
using Application.results.common;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedListRequestByPublicityHandler : IRequestHandler<GetTestDetailedListRequestByPublicity, BaseCommandResult<List<RespondTestDto>>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedListRequestByPublicityHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResult<List<RespondTestDto>>> Handle(GetTestDetailedListRequestByPublicity request, CancellationToken cancellationToken)
    {
        var tests = await _repository.GetTestWithDetailsByPublicity();
        
        var respondTestDtos = _mapper.Map<List<RespondTestDto>>(tests);
        return new OkSuccessStatusResult<List<RespondTestDto>>(respondTestDtos);
    }
}