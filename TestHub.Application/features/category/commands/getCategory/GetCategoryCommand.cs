using Application.common.models;
using Domain.entities;

namespace Application.features.category.commands.getCategory;

public abstract record GetCategoryCommand(int Id) : IRequest<Result<Category>>;