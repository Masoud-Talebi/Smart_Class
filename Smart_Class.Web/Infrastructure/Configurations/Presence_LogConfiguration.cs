using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smart_Class.Web.Core.Domain;

namespace Smart_Class.Web.Infrastructure.Configurations
{
    public class Presence_LogConfiguration : IEntityTypeConfiguration<Presence_Log>
    {
        public void Configure(EntityTypeBuilder<Presence_Log> builder)
        {

        }

    }
}