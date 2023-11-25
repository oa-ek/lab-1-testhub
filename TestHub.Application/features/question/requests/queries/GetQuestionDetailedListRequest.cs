using Application.dtos;
using Application.dtos.requestsDto;

namespace Application.features.question.requests.queries;

public class GetQuestionDetailedListRequest  : IRequest<List<RequestQuestionDto>> {}