using Smart_Class.Web.Application.Dtos;
using System;
using System.Threading.Tasks;
namespace Smart_Class.Web.Application.Contracts;

public interface IClassService
{
    Task<IEnumerable<ClassDto>> GetAllClasses(string Title = "");
    Task<ClassDto> GetClassById(Guid ClassId);
    Task<ClassDto> AddClass(AddClassDto addClass);
    Task<ClassDto> UpdateClass(UpdateClassDto updateClass);
    Task RemoveClass(Guid ClassId);
}
