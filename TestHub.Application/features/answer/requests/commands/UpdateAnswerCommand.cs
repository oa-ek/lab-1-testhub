﻿namespace Application.features.answer.requests.commands;

public class UpdateAnswerCommand: IRequest<BaseCommandResponse<RespondAnswerDto>>
{
    public int Id { get; set; }
    public required RequestAnswerDto? AnswerDto { get; set; }
}