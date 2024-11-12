using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;
using Smart_Class.Web.Infrastructure;

namespace Smart_Class.Web.Application.Services;

public class ClassService : IClassService
{
    #region Fields
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public ClassService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    #endregion

    #region Get Funcs
    public async Task<IEnumerable<ClassDto>> GetAllClasses(string Title = "")
    {
        var res = _context.Classes.Where(p => p.Deleted == false).Include(p => p.Teacher).AsQueryable();
        if (Title != "")
            res = res.Where(p=>p.Name.ToLower().Trim().Contains(Title.ToLower().Trim()));
        return _mapper.Map<IEnumerable<ClassDto>>(res.ToList());
    }

    public async Task<ClassDto> GetClassById(Guid ClassId)
    {
        var res = await _context.Classes.Where(p => p.Deleted == false).FirstOrDefaultAsync(p => p.Id == ClassId);
        return _mapper.Map<ClassDto>(res);
    }
    #endregion

    #region Post Funcs
    public async Task RemoveClass(Guid ClassId)
    {
        var class_room = await _context.Classes.FirstOrDefaultAsync(p => p.Id == ClassId);
        class_room.Deleted = true;
        _context.Classes.Update(class_room);
        await _context.SaveChangesAsync();
    }

    public async Task<ClassDto> UpdateClass(UpdateClassDto updateClass)
    {
        var Modifyclass = _mapper.Map<Class>(updateClass);
        _context.Classes.Update(Modifyclass);
        await _context.SaveChangesAsync();
        return _mapper.Map<ClassDto>(Modifyclass);
    }
    public async Task<ClassDto> AddClass(AddClassDto addClass)
    {
        var Createclass = _mapper.Map<Class>(addClass);
        await _context.Classes.AddAsync(Createclass);
        await _context.SaveChangesAsync();
        return _mapper.Map<ClassDto>(Createclass);
    }
    #endregion
}
