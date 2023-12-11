using Application.features.test.requests.queries;
using Application.results.common;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedListRequestByUserHandler : IRequestHandler<GetTestDetailedListRequestByUser, BaseCommandResult<List<RespondTestDto>>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedListRequestByUserHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResult<List<RespondTestDto>>> Handle(GetTestDetailedListRequestByUser request, CancellationToken cancellationToken)
    {
        var tests = await _repository.GetTestWithDetailsByUser(request.OwnerId);

        var respondTestDtos = _mapper.Map<List<RespondTestDto>>(tests);
        return new OkSuccessStatusResult<List<RespondTestDto>>(respondTestDtos);
    }
}