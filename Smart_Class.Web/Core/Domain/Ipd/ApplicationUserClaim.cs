using Microsoft.AspNetCore.Identity;

namespace Smart_Class.Web.Core.Domain.Ipd;

public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    public virtual Teacher User { get; set; }
}