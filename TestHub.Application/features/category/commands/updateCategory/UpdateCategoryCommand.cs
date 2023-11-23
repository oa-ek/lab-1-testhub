using Application.common.models;
using Domain.entities;

namespace Application.features.category.commands.updateCategory;

public abstract record UpdateCategoryCommand(int id, CategoryDto CategoryDto) : IRequest<Result<Category>> { }