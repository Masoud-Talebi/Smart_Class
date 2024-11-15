using Smart_Class.Web.Application.Dtos;

namespace Smart_Class.Web.Application.Contracts
{
    public interface IPresenceService
    {
        Task<IEnumerable<PresenceDto>> GetAll(DateTime date, string Title = "");
        Task<IEnumerable<PresenceDto>> GetByClass(Guid ClassId, DateTime date,string Title = "");
    }
}
