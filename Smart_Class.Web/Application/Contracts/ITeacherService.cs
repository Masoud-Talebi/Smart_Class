using Smart_Class.Web.Application.Dtos;

namespace Smart_Class.Web.Application.Contracts
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllTeacher(string? Title = "");
        Task<TeacherDto> GetTeacherById(Guid Id);
        Task<TeacherDto> AddTeacher(AddTeacherDto addTeacher);
        Task<TeacherDto> UpdateTeacher(UpdateTeacherDto updateTeacher);
        Task RemoveTeacher(Guid Id);
    }
}
