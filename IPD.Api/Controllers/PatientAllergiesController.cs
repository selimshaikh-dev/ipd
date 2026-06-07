using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    public class PatientAllergiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}