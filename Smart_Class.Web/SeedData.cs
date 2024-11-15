using Smart_Class.Web.Core.Domain.Ipd;
using System.Data;

namespace Smart_Class.Web
{
    public static class SeedData
    {
        public static IEnumerable<ApplicationRole> DefaultRoles =>
            new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = SD.Teacher,
                    NormalizedName =  SD.Teacher,
                    Persian_Name = "استاد"
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = SD.Admin,
                    NormalizedName = SD.Admin,
                    Persian_Name = "مدیر سیستم"
                },
            };

    }
}
