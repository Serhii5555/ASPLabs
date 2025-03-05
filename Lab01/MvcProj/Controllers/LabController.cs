using Microsoft.AspNetCore.Mvc;

namespace MvcProj.Controllers
{
    public class LabController : Controller
    {
        public IActionResult Info()
        {
            var model = new
            {
                LabNumber = "1",
                Topic = "Project Creation in .NET",
                Purpose = "Learn to create console, API, and MVC projects using dotnet CLI",
                Name = "Serhii Beilakh"
            };
            return View(model);
        }
    }
}
