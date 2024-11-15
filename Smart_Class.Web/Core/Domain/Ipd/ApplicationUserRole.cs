using Microsoft.AspNetCore.Identity;

namespace Smart_Class.Web.Core.Domain.Ipd;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public virtual Teacher User { get; set; }
    public virtual ApplicationRole Role { get; set; }
}