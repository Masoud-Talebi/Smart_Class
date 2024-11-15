using AutoMapper;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;

namespace Smart_Class.Web.Application.Mapping;

public class Presence_LogProfile : Profile
{
    public Presence_LogProfile()
    {
        CreateMap<Presence_Log, PresenceDto>()
        .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
        .ForMember(dest => dest.ClassCode, opt => opt.MapFrom(src => src.Class.Code));
    }

}
