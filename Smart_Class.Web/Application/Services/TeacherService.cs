using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;
using Smart_Class.Web.Infrastructure;

namespace Smart_Class.Web.Application.Services
{
    public class TeacherService : ITeacherService
    {
        #region Fields
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TeacherService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Get Func
        public async Task<IEnumerable<TeacherDto>> GetAllTeacher(string? Title = "")
        {
            var res = _context.Teachers.Where(P => P.Deleted == false).AsQueryable();
            if (Title != null) res = res.Where(p => p.LastName.ToLower().Trim().Contains(Title.ToLower().Trim()) || p.FirstName.ToLower().Trim().Contains(Title.ToLower().Trim()));
            return _mapper.Map<IEnumerable<TeacherDto>>(res);
        }
        public async Task<TeacherDto> GetTeacherById(Guid Id)
        {
            var res = await _context.Teachers.Where(p => p.Deleted == false).FirstOrDefaultAsync(p => p.Id == Id);
            return _mapper.Map<TeacherDto>(res);
        }
        public async Task<IEnumerable<ClassDto>> getAllClassByTeacherId(Guid Teacherid)
        {
            var res = await _context.Classes.Where(P => P.Deleted == false && P.TeacherId == Teacherid).ToListAsync();
            return _mapper.Map<IEnumerable<ClassDto>>(res);
        }

        #endregion

        #region Post Func
        public async Task<TeacherDto> AddTeacher(AddTeacherDto addTeacher)
        {
            var CreateTeacher = _mapper.Map<Teacher>(addTeacher);
            await _context.Teachers.AddAsync(CreateTeacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeacherDto>(CreateTeacher);
        }
        public async Task RemoveTeacher(Guid Id)
        {
            var Teacher = await _context.Teachers.FirstOrDefaultAsync(p => p.Id == Id);
            Teacher.Deleted = true;
            _context.Teachers.Update(Teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<TeacherDto> UpdateTeacher(UpdateTeacherDto updateTeacher)
        {
            var ModifyTeacher = _mapper.Map<Teacher>(updateTeacher);
            _context.Teachers.Update(ModifyTeacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeacherDto>(ModifyTeacher);
        }

        #endregion

    }
}
