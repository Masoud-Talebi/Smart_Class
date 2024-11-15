


using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Smart_Class.Web;
using Smart_Class.Web.Core.Domain;
using System.Security.Claims;

namespace Smart_Class.Web.Application.Initializer
{
    public class DbInitializer : IDbinitializer
    {
        #region Fileds

        private UserManager<Teacher> userManager;

        public DbInitializer(UserManager<Teacher> userManager)
        {
            this.userManager = userManager;
        }

        #endregion
        public async Task Initialize()
        {

            Teacher user = new Teacher
            {
                FirstName = "pouria",
                LastName = "Rahnama",
                UserName = "pouriarahnama",
                EmailConfirmed = true,
                SSID = "1274685591"
            };
            var result = await userManager.CreateAsync(user, "Masoud@2023");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, SD.Admin);

                var temp1 = userManager.AddClaimsAsync(user, new Claim[]
                {
                         new Claim(JwtClaimTypes.Name, user.FirstName + " " + user.LastName),
                         new Claim(JwtClaimTypes.GivenName, user.FirstName),
                         new Claim(JwtClaimTypes.FamilyName, user.LastName),
                         new Claim(JwtClaimTypes.Subject, user.SSID),
                         new Claim(JwtClaimTypes.Role, SD.Admin),
                }).Result;

            }





        }
    }
}
