using Application.features.test.requests.queries;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedListRequestByUserHandler : IRequestHandler<GetTestDetailedListRequestByUser, BaseCommandResponse<List<RespondTestDto>>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedListRequestByUserHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<List<RespondTestDto>>> Handle(GetTestDetailedListRequestByUser request, CancellationToken cancellationToken)
    {
        var tests = await _repository.GetTestWithDetailsByUser(request.OwnerId);

        var respondTestDtos = _mapper.Map<List<RespondTestDto>>(tests);
        return new OkSuccessStatusResponse<List<RespondTestDto>>(respondTestDtos);
    }
}