using Application.persistence.dtos;
using Domain.entities;

namespace Application.persistence.profiles;

public class MappingProfile : Profile
{
    protected MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}