namespace Application.profiles;

public class MappingAnswerProfile : Profile
{
    public MappingAnswerProfile()
    {
        CreateMap<Answer, RequestAnswerDto>().ReverseMap();
        CreateMap<Answer, RespondAnswerDto>().ReverseMap();
    }
}