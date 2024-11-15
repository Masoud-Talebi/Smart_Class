using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain.Ipd;

namespace Smart_Class.Web.Application.Contracts
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllTeacher(string? Title = "");
        Task<IEnumerable<ClassDto>> getAllClassByTeacherId(Guid Teacherid);
        Task<TeacherDto> GetTeacherById(Guid Id);
        Task<List<string>> AddTeacher(AddTeacherDto addTeacher);
        Task UpdateTeacher(UpdateTeacherDto updateTeacher);
        Task RemoveTeacher(Guid Id);
        Task<bool> SigninPersonel(SigninUserDto userDto);
        Task<bool> ChangePassKey(ChangePasswordDto change);
        Task<IEnumerable<ApplicationRole>> GetaAllRole();
    }
}
