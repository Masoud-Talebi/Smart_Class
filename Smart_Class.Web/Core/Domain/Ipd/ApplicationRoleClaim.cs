using Microsoft.AspNetCore.Identity;

namespace Smart_Class.Web.Core.Domain.Ipd;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual ApplicationRole Role { get; set; }
}