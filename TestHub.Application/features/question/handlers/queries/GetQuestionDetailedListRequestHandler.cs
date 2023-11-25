﻿using Application.dtos;
using Application.features.question.requests.queries;
using Application.persistence.contracts;

namespace Application.features.question.handlers.queries;

public class GetQuestionDetailedListRequestHandler : IRequestHandler<GetQuestionDetailedListRequest, List<QuestionDto>>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public GetQuestionDetailedListRequestHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<QuestionDto>> Handle(GetQuestionDetailedListRequest request, CancellationToken cancellationToken)
    {
        var questions = await _repository.GetQuestionsWithDetails();

        return _mapper.Map<List<QuestionDto>>(questions);
    }
}