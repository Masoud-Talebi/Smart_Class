using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Infrastructure;

namespace Smart_Class.Web.Application.Services
{
    public class PresenceService : IPresenceService
    {
        #region Fields
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PresenceService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion
        public async Task<IEnumerable<PresenceDto>> GetAll(DateTime date, string Title = "")
        {
            var res = _context.Presence_Logs.Include(p => p.Class).AsQueryable();
            if (Title != null) 
                res = res.Where(p => p.LastName.ToLower().Trim().Contains(Title.ToLower().Trim()) || p.FirstName.ToLower().Trim().Contains(Title.ToLower().Trim()));
            if (date != DateTime.MinValue)
                res = res.Where(p => p.CreateAt.Year == date.Year && p.CreateAt.Month == date.Month && p.CreateAt.Day == date.Day);
            return _mapper.Map<IEnumerable<PresenceDto>>(await res.ToListAsync());
        }

        public async Task<IEnumerable<PresenceDto>> GetByClass(Guid ClassId, DateTime date, string Title = "")
        {
            var res = _context.Presence_Logs.Where(p=>p.ClassId == ClassId).Include(p => p.Class).AsQueryable();
            if (Title != null)
                res = res.Where(p => p.LastName.ToLower().Trim().Contains(Title.ToLower().Trim()) || p.FirstName.ToLower().Trim().Contains(Title.ToLower().Trim()));
            if(date != DateTime.MinValue)
                res = res.Where(p => p.CreateAt.Year == date.Year && p.CreateAt.Month == date.Month && p.CreateAt.Day == date.Day);
            return _mapper.Map<IEnumerable<PresenceDto>>(await res.ToListAsync());
        }
    }
}
