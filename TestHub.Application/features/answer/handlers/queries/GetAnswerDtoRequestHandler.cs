﻿using Application.contracts.persistence;
using Application.dtos.respondsDto;
using Application.features.answer.requests.queries;

namespace Application.features.answer.handlers.queries;

public class GetAnswerDtoRequestHandler : IRequestHandler<GetAnswerDtoRequest, RespondAnswerDto>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerDtoRequestHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<RespondAnswerDto> Handle(GetAnswerDtoRequest request, CancellationToken cancellationToken)
    {
        var answer = await _repository.Get(request.Id);
        
        return _mapper.Map<RespondAnswerDto>(answer);
    }
}