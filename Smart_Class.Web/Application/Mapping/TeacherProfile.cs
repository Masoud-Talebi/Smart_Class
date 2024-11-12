using AutoMapper;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;

namespace Smart_Class.Web.Application.Mapping;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<Teacher, TeacherDto>();

        CreateMap<AddTeacherDto, Teacher>();
        CreateMap<UpdateTeacherDto, Teacher>();
    }
}
