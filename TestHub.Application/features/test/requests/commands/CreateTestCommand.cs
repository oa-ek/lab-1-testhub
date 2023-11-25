﻿using Application.dtos.requestsDto;

namespace Application.features.test.requests.commands;

public class CreateTestCommand : IRequest<int>
{
    public required RequestTestDto TestDto { get; set; }
}