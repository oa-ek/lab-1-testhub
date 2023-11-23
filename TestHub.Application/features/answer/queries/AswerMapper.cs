using Application.common.models;
using Domain.entities;

namespace Application.features.answer.queries;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AnswerDto, Answer>()
            .ForMember(dest => dest.ImageUrl, 
                opt => opt.MapFrom(src => MapFileDtoToImageUrl(src.Image)));

        CreateMap<Answer, AnswerDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => MapImageUrlToFileDto(src.ImageUrl)));
    }

    private string MapFileDtoToImageUrl(FileDto? fileDto)
    {
        if (fileDto != null)
        {
            // Викликати метод для завантаження файлу на віддалений сервер
            // і повернути шлях до завантаженого файлу
        }

        return null;
    }

    private FileDto MapImageUrlToFileDto(string? imageUrl)
    {
        if (imageUrl != null)
        {
            // Викликати метод для отримання FileDto на основі шляху до файла
        }

        return null;
    }
}
