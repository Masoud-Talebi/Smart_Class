using AutoMapper;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;

namespace Smart_Class.Web.Application.Mapping;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<Teacher, TeacherDto>()
            .ForMember(dest => dest.Persian_RoleName, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role.Persian_Name))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role.Name));

        CreateMap<AddTeacherDto, Teacher>();
        CreateMap<UpdateTeacherDto, Teacher>();
    }
}
