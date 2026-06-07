using IPD.Admin.Utilities.Encryption;
using IPD.Domain.Dto;
using IPD.Admin.Extensions;
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Admin.Controllers
{
    public class RecoveryRequestController : Controller
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public RecoveryRequestController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ReportPickerDto datePicker)
        {
            List<RecoveryRequest> recoveryRequest = context.RecoveryRequests
                .AsNoTracking()
                .Where(m => ((m.DateRequested) >= (datePicker.DateFrom) && (m.DateRequested) <= (datePicker.DateTo)) && m.IsTicketOpen == true)
                .Include(r => r.UserAccounts)
                .ToList();

            ViewBag.RecoveryRequestList = recoveryRequest;

            if (recoveryRequest.Count == 0)
                ViewBag.Message = "No match found!";

            return View();
        }

        [HttpGet]
        public IActionResult UpdatePassword(Guid? id, Guid? requestID)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                if (requestID == null)
                    return BadRequest();

                UserAccount user = context.UserAccounts.Find(id);
                RecoveryRequest recoveryRequest = context.RecoveryRequests.Find(requestID);

                if (user == null || recoveryRequest == null)
                    return NotFound();

                if (user.UserType == UserType.Administrator)
                    //return BadRequest();
                return StatusCode(StatusCodes.Status400BadRequest, "Access Denied!");

                string decryptedPassword = string.Empty;

                EncryptionHelpers encryptionHelper = new EncryptionHelpers();
                decryptedPassword = encryptionHelper.Decrypt(user.Password);

                user.Password = decryptedPassword;
                //user.ConfirmPassword = decryptedPassword;

                ViewBag.RecoveryRequestID = recoveryRequest.RecoveryRequestID;

                return View(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePassword(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                EncryptionHelpers encryptionHelper = new EncryptionHelpers();
                string encryptedPassword = string.Empty;
                user.Password = encryptionHelper.Encrypt(user.Password);

                var adminUser = session?.GetCurrentAdmin();
                user.ModifiedBy = adminUser.UserAccountID;
                user.Password = user.Password;
                //user.ConfirmPassword = encryptedPassword;
                user.DateModified = DateTime.Now;
                user.SyncStatus = RowSyncStatus.NotSynced;

                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();

                if (!string.IsNullOrEmpty(Request.Form["recoveryrequestid"].ToString()))
                {
                    Guid rid = Guid.Parse(Request.Form["recoveryrequestid"]);

                   RecoveryRequest recoveryrequestindb = context.RecoveryRequests.FirstOrDefault(r => r.RecoveryRequestID == rid);

                   if (recoveryrequestindb != null)
                   {
                       recoveryrequestindb.IsTicketOpen = false;
                       context.Entry(recoveryrequestindb).State = EntityState.Modified;
                       context.SaveChanges();
                   }
                }

                TempData["Message"] = "User account updated successfully!";
                return RedirectToAction("Index");
            }

            return View(user);
        }
    }
}