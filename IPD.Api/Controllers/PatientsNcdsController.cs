using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    public class PatientsNcdsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}