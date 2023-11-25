using Application.common.models;
using Domain.entities;

namespace Application.features.category.commands.deleteCategory
{
    public record DeleteCategoryCommand(int Id) : IRequest<Result<Category>>;
}