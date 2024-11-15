using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smart_Class.Web.Core;
using Smart_Class.Web.Core.Domain;
using Smart_Class.Web.Core.Domain.Ipd;
using Smart_Class.Web.Infrastructure.Configurations;

namespace Smart_Class.Web.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Teacher, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ClassConfiguration).Assembly);
            // modelBuilder.OnCreated();
            // modelBuilder.OnDeleted();
            // modelBuilder.OnUpdated();
            builder.Entity<ApplicationRole>(p=>
            {
                p.HasData(SeedData.DefaultRoles);
            });
            builder.Entity<Teacher>(b =>
            {
                b.ToTable("Users");
                b.Property(user => user.Email).HasMaxLength(260);
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("Roles");
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

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