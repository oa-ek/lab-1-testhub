using Application.features.test.requests.queries;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedListRequestByPublicityHandler : IRequestHandler<GetTestDetailedListRequestByPublicity, BaseCommandResponse<List<RespondTestDto>>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedListRequestByPublicityHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<List<RespondTestDto>>> Handle(GetTestDetailedListRequestByPublicity request, CancellationToken cancellationToken)
    {
        var tests = await _repository.GetTestWithDetailsByPublicity();
        
        var respondTestDtos = _mapper.Map<List<RespondTestDto>>(tests);
        return new OkSuccessStatusResponse<List<RespondTestDto>>(respondTestDtos);
    }
}