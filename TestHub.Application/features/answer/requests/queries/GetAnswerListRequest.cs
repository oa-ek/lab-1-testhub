using Application.dtos;
using Application.dtos.requestsDto;

namespace Application.features.answer.requests.queries;

public class GetAnswerListRequest : IRequest<List<RequestAnswerDto>> {}