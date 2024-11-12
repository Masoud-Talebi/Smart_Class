using Microsoft.AspNetCore.Mvc;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Application.Dtos;

namespace Smart_Class.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        public TeacherController(IClassService classService, ITeacherService teacheService)
        {
            _classService = classService;
            _teacherService = teacheService;
        }
        public async Task<IActionResult> Index(string Title = "")
        {
            try
            {
                if (Title != null)
                    ViewBag.Title = Title;
                var res = await _teacherService.GetAllTeacher(Title);
                return View(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public IActionResult CreateTeacher()
        {
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
            return View(Teacher);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherDto updateTeacher)
        {
            try
            {
                var res = await _teacherService.UpdateTeacher(updateTeacher);
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