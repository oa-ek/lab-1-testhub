using Application.persistence.dtos;

namespace Application.features.category.requests.commands;

public class CreateCategoryCommand : IRequest<int>
{
    public CategoryDto CategoryDto { get; set; }
}