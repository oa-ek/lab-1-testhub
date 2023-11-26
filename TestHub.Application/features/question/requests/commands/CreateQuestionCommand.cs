﻿using Application.dtos.requestsDto;

namespace Application.features.question.requests.commands;

public class CreateQuestionCommand : IRequest<BaseCommandResponse>
{
    public required RequestQuestionDto QuestionDto { get; set; }
}