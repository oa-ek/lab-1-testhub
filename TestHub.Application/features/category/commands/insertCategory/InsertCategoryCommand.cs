using Application.common.models;
using Domain.entities;

namespace Application.features.category.commands.insertCategory;

public abstract record InsertCategoryCommand(CategoryDto CategoryDto) : IRequest<Result<Category>> { }
