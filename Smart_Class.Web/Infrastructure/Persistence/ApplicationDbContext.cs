using Microsoft.EntityFrameworkCore;
using Smart_Class.Web.Core;
using Smart_Class.Web.Core.Domain;
using Smart_Class.Web.Infrastructure.Configurations;

namespace Smart_Class.Web.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClassConfiguration).Assembly);
            // modelBuilder.OnCreated();
            // modelBuilder.OnDeleted();
            // modelBuilder.OnUpdated();
        }

        #region Tables

        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Presence_Log> Presence_Logs { get; set; }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateAt = DateTime.Now;

                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeleteAt = DateTime.Now;

                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateAt = DateTime.Now;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<IDateEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateAt = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeleteAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}