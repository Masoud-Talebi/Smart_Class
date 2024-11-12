using Microsoft.AspNetCore.Mvc;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Application.Dtos;

namespace Smart_Class.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITeacherService _teacheService;
        public TeacherController(IClassService classService, ITeacherService teacheService)
        {
            _classService = classService;
            _teacheService = teacheService;
        }
        public async Task<IActionResult> Index(string Title = "")
        {
            try
            {
                if (Title != null)
                    ViewBag.Title = Title;
                var res = await _teacheService.GetAllTeacher(Title);
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
                var res = await _teacheService.AddTeacher(addTeacher);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}