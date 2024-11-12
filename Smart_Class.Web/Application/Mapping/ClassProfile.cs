using AutoMapper;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;

namespace Smart_Class.Web.Application.Mapping;

public class ClassProfile : Profile
{
    public ClassProfile()
    {
        CreateMap<Class, ClassDto>().ReverseMap();
        //.ForMember(dest => dest.End_Time, opt => opt.MapFrom(src => src.End_Time))
        //.ForMember(dest => dest.start_Time, opt => opt.MapFrom(src => src.start_Time))
        //.ForMember(dest => dest.Time_Holding, opt => opt.MapFrom(src => src.Time_Holding));

        CreateMap<AddClassDto, Class>();
        CreateMap<UpdateClassDto, Class>();
    }
}
