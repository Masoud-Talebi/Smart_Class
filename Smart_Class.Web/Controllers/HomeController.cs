using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;
using Smart_Class.Web.Models;
using System.Diagnostics;

namespace Smart_Class.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<Teacher> _signInManager;
        private readonly UserManager<Teacher> _userManager;
        public HomeController(ITeacherService teacheService, ILogger<HomeController> logger, SignInManager<Teacher> signInManager, UserManager<Teacher> userManager)
        {
            _teacherService = teacheService;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        #region Login
        [AllowAnonymous]
        [Route("/Login")]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return (User.Identity.IsAuthenticated) ? Redirect("/") : View();
        }

        [AllowAnonymous]
        [Route("/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SigninUserDto user)
        {
            try
            { 
                var useer = await _userManager.FindByNameAsync(user.UserName);
                if (useer != null) 
                {
                    if (useer.Deleted == false)
                    {
                        var result = await _teacherService.SigninPersonel(user);
                        if (result) return Redirect("/");
                    }
                }

            }
            catch (Exception ex)
            {
                return View(user);
            }
            ViewData["ErrorMessage"] = "اطلاعات حساب کاربری صحیح نمی باشد";
            return await Login();
        }
        #endregion

        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect($"/");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
