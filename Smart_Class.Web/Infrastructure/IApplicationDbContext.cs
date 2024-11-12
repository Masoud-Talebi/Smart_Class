using Microsoft.EntityFrameworkCore;
using Smart_Class.Web.Core.Domain;

namespace Smart_Class.Web.Infrastructure
{
    public interface IApplicationDbContext
    {
        #region Tables
        DbSet<Class> Classes { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<Presence_Log> Presence_Logs { get; set; }

        #endregion


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}