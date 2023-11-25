﻿using Application.dtos.requestsDto;

namespace Application.features.answer.requests.commands;

public class CreateAnswerCommand : IRequest<int>
{
    public required RequestAnswerDto AnswerDto { get; set; }
}