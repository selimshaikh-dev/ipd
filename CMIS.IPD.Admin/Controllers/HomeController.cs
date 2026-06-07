using IPD.Admin.Models;
using IPD.Admin.Utilities.Encryption;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using IPD.Admin.Extensions;
using static IPD.Domain.Constants.Enumerators;
using IPD.Domain.Dto;
using IPD.Admin.Models;
using CMIS.IPD.Admin.Models;

namespace IPD.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<HomeController> logger;

        private ISession? session => httpContextAccessor.HttpContext?.Session;
        //private readonly HttpClient httpClient;

        public HomeController(DataContext context, IHttpContextAccessor httpContextAccessor,ILogger<HomeController> logger)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //Session["AdminSession"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel userLogin)
        {
            bool blnFlag = false;

            try
            {
                if (ModelState.IsValid)
                {
                    var userInDb = context.UserAccounts
                        .AsNoTracking()
                        .Where(u => u.Username.ToUpper() == userLogin.UserName.ToUpper() && u.AccountStatus == RowStatus.Active)
                        .FirstOrDefault();

                    if (userInDb != null)
                    {
                        if (userInDb.UserType != UserType.GeneralUser)
                        {
                            EncryptionHelpers encryptionHelpers = new EncryptionHelpers();

                            string decryptedPassword = encryptionHelpers.Decrypt(userInDb.Password);

                            if (decryptedPassword == userLogin.Password)
                            {
                                blnFlag = true;
                            }
                        }
                    }

                    if (blnFlag == true)
                    {
                        session?.SetCurrentAdmin(userInDb);
                        return RedirectToAction("Console", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid username or password!";
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        public IActionResult Console()
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