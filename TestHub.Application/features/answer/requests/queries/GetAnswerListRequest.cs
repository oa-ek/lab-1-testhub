using Application.dtos;

namespace Application.features.answer.requests.queries;

public class GetAnswerListRequest : IRequest<List<AnswerDto>> {}