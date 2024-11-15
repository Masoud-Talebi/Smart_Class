using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;

namespace Smart_Class.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        private readonly UserManager<Teacher> _userManager;
        public TeacherController(IClassService classService, ITeacherService teacheService, UserManager<Teacher> userManager)
        {
            _classService = classService;
            _teacherService = teacheService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string Title = "")
        {
            try
            {
                if (Title != "")
                    ViewBag.Title = Title;
                var res = await _teacherService.GetAllTeacher(Title);
                return View(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> CreateTeacher()
        {
            var roles = await _teacherService.GetaAllRole();
            ViewBag.Roles = new SelectList(roles, "Name", "Persian_Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(AddTeacherDto addTeacher)
        {
            try
            {
              
                var res = await _teacherService.AddTeacher(addTeacher);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> UpdateTeacher(Guid Id)
        {
            
            var Teacher = await _teacherService.GetTeacherById(Id);
            var roles = await _teacherService.GetaAllRole();
            ViewBag.Roles = new SelectList(roles, "Name", "Persian_Name", Teacher.RoleName);
            return View(Teacher);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherDto updateTeacher)
        {
            try
            {
                await _teacherService.UpdateTeacher(updateTeacher);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTeacher(Guid Id)
        {
            await _teacherService.RemoveTeacher(Id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ViewClass(Guid TeacherId)
        {
            try
            {
                var res = await _teacherService.getAllClassByTeacherId(TeacherId);
                return View(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}