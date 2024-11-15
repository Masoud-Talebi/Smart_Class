using Microsoft.AspNetCore.Identity;
using Smart_Class.Web.Core;
using Smart_Class.Web.Core.Domain.Ipd;

namespace Smart_Class.Web.Core.Domain
{
    public class Teacher : IdentityUser<Guid>
    {
        public bool Deleted { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string SSID { get; set; }

        //Navigations
        public ICollection<Class>? Classes { get; set; }

        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}