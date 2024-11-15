using Microsoft.AspNetCore.Identity;

namespace Smart_Class.Web.Core.Domain.Ipd;

public class ApplicationUserToken : IdentityUserToken<Guid>
{
    public virtual Teacher User { get; set; }
}