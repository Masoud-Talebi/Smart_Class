using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Application.Dtos;
using Smart_Class.Web.Core.Domain;

namespace Smart_Class.Web.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITeacherService _teacheService;
        public ClassController(IClassService classService, ITeacherService teacheService)
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
                var res = await _classService.GetAllClasses(Title);
                return View(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> CreateClass()
        {
            var teacher = await _teacheService.GetAllTeacher();
            ViewBag.teachers = new SelectList(teacher, "Id", "FullName");
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateClass(AddClassDto addClass)
        {
            try
            {
                var res = await _classService.AddClass(addClass);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> UpdateClass(Guid Id)
        {
            var teacher = await _teacheService.GetAllTeacher();
            var Class = await _classService.GetClassById(Id);
            ViewBag.teachers = new SelectList(teacher, "Id", "FullName", Class.TeacherId);
            
            return View(Class);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateClass(UpdateClassDto updateClass)
        {
            try
            {
                var res = await _classService.UpdateClass(updateClass);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteClass(Guid Id){
            await _classService.RemoveClass(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
