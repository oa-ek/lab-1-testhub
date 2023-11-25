﻿using Application.persistence.dtos;

namespace Application.features.category.requests.commands;

public class CreateCategoryCommand : IRequest<int>
{
    public required CategoryDto CategoryDto { get; set; }
}