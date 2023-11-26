﻿using Application.dtos.requestsDto;

namespace Application.features.answer.requests.commands;

public class UpdateAnswerCommand: IRequest<BaseCommandResponse>
{
    public int Id { get; set; }
    public required RequestAnswerDto AnswerDto { get; set; }
}