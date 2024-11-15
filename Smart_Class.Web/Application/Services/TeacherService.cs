using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;
using Smart_Class.Web.Core.Domain.Ipd;
using Smart_Class.Web.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Smart_Class.Web.Application.Services
{
    public class TeacherService : ITeacherService
    {
        #region Fields
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Teacher> _userManager;
        private readonly SignInManager<Teacher> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public TeacherService(IApplicationDbContext context, IMapper mapper, UserManager<Teacher> userManager, SignInManager<Teacher> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Get Func
        public async Task<IEnumerable<TeacherDto>> GetAllTeacher(string? Title = "")
        {
            var res = _context.Teachers.Where(P => P.Deleted == false).Include(p=>p.UserRoles).ThenInclude(p=>p.Role).AsQueryable();
            if (Title != null) res = res.Where(p => p.LastName.ToLower().Trim().Contains(Title.ToLower().Trim()) || p.FirstName.ToLower().Trim().Contains(Title.ToLower().Trim()));
            return _mapper.Map<IEnumerable<TeacherDto>>(res);
        }
        public async Task<TeacherDto> GetTeacherById(Guid Id)
        {
            var res = await _context.Teachers.Where(p => p.Deleted == false).Include(p => p.UserRoles).ThenInclude(p => p.Role).FirstOrDefaultAsync(p => p.Id == Id);
            return _mapper.Map<TeacherDto>(res);
        }
        public async Task<IEnumerable<ClassDto>> getAllClassByTeacherId(Guid Teacherid)
        {
            var res = await _context.Classes.Where(P => P.Deleted == false && P.TeacherId == Teacherid).ToListAsync();
            return _mapper.Map<IEnumerable<ClassDto>>(res);
        }
        public async Task<IEnumerable<ApplicationRole>> GetaAllRole()
        {
            return await _roleManager.Roles.Select(p => new ApplicationRole
            {
                Name = p.Name,
                NormalizedName = p.NormalizedName,
                Persian_Name = p.Persian_Name,
                Id = p.Id
            }).ToListAsync();
        }

        #endregion

        #region Post Func
        public async Task<List<string>> AddTeacher(AddTeacherDto addTeacher)
        {
            try
            {
                Teacher user = new Teacher
                {
                    UserName = addTeacher.UserName,
                    FirstName = addTeacher.FirstName,
                    LastName = addTeacher.LastName,
                    SSID = addTeacher.SSID,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, addTeacher.Password);
                if (result.Succeeded)
                {
                    var resultRole = _userManager.AddToRoleAsync(user, addTeacher.RoleName).GetAwaiter().GetResult();
                    if (resultRole.Succeeded)
                    {
                        await _userManager.AddClaimsAsync(user, new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name,user.FirstName + " " + user.LastName),
                            new Claim(JwtClaimTypes.GivenName,user.FirstName),
                            new Claim(JwtClaimTypes.FamilyName,user.LastName),
                            new Claim(JwtClaimTypes.Role,addTeacher.RoleName),
                            new Claim(JwtClaimTypes.Subject,user.SSID)
                        });
                    }
                }
                else
                {
                    var errors = new List<string>();
                    foreach (var error in result.Errors)
                        errors.Add(error.Description);

                    return errors;
                }

                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
        public async Task RemoveTeacher(Guid Id)
        {
            var Teacher = await _context.Teachers.FirstOrDefaultAsync(p => p.Id == Id);
            Teacher.Deleted = true;
            _context.Teachers.Update(Teacher);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Claim>> UserClaims(Teacher user)
        => await _userManager.GetClaimsAsync(user);

        public async Task UpdateTeacher(UpdateTeacherDto updateTeacher)
        {
            var user = await _userManager.FindByIdAsync(updateTeacher.Id.ToString());
            var cliams = await UserClaims(user);

            if (user != null)
            {
                user.FirstName = updateTeacher.FirstName;
                user.LastName = updateTeacher.LastName;
                user.SSID = updateTeacher.SSID;
                user.UserName = updateTeacher.UserName;
                user.EmailConfirmed = true;
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var role = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, role);
                var resultRole = _userManager.AddToRoleAsync(user, updateTeacher.RoleName).GetAwaiter().GetResult();
                var resultRemoveClimes = await _userManager.RemoveClaimsAsync(user, cliams);
                if (resultRemoveClimes.Succeeded)
                    await _userManager.AddClaimsAsync(user, new Claim[]
                          {
                            new Claim(JwtClaimTypes.Name,user.FirstName + " " + user.LastName),
                            new Claim(JwtClaimTypes.GivenName,user.FirstName),
                            new Claim(JwtClaimTypes.FamilyName,user.LastName),
                            new Claim(JwtClaimTypes.Role,updateTeacher.RoleName),
                            new Claim(JwtClaimTypes.Subject,user.SSID)
                          });
            }
            await _userManager.UpdateSecurityStampAsync(user);
        }

        public async Task<bool> SigninPersonel(SigninUserDto userDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userDto.UserName);
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("teacher") || userRoles.Contains("admin"))
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userDto.Password, userDto.RememberMe,
                        lockoutOnFailure: true);

                    if (result.IsLockedOut)
                        throw new Exception($"Account {userDto.UserName} is Lock out");
                    return result.Succeeded;

                }
                else
                {
                    throw new Exception("The User does not have access to enter this panel.");

                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ChangePassKey(ChangePasswordDto change)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(change.UserId);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, change.Password, change.NewPassword);
                    if (result.Succeeded) return true;
                }

                throw new Exception("کاربر مورد نظر یافت نشد");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

    }
}
