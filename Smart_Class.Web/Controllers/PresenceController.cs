using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Core.Domain;

namespace Smart_Class.Web.Controllers
{
    [Authorize(Roles = SD.Admin)]
    public class PresenceController : Controller
    {
        private readonly IClassService _classService;
        private readonly IPresenceService _presenceService;
        public PresenceController(IClassService classService, IPresenceService presenceService)
        {
            _classService = classService;
            _presenceService = presenceService;
        }
        public async Task<IActionResult> Index(DateTime date, string Title, Guid ClassId)
        {
            var classes =await _classService.GetAllClasses();
            ViewBag.Classes = new SelectList(classes, "Id", "Name");
            if (date != null) ViewBag.Date = date;
            if (ClassId != null && ClassId != Guid.Empty)
            {
                ViewBag.Classes = new SelectList(classes, "Id", "Name", ClassId);
                if (Title != "")
                    ViewBag.Title = Title;
                var res = await _presenceService.GetByClass(ClassId, date, Title);
                 return View(res);
            }
            else
            {
                if (Title != "")
                    ViewBag.Title = Title;
                var res =await _presenceService.GetAll(date, Title);
                return View(res);
            }
            
        }
    }
}
