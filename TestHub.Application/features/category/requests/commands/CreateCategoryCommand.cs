using Application.dtos.sharedDTOs;

namespace Application.features.category.requests.commands;

public class CreateCategoryCommand : IRequest<BaseCommandResponse>
{
    public required CategoryDto CategoryDto { get; set; }
}