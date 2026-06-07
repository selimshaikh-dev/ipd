using IPD.Admin.Extensions;
using IPD.Admin.Utilities.Encryption;
using IPD.Domain.Constants;
using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Admin.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<UserAccountsController> logger;

        private ISession? session => httpContextAccessor.HttpContext?.Session;
        //private readonly HttpClient httpClient;

        public UserAccountsController(DataContext context, IHttpContextAccessor httpContextAccessor,ILogger<UserAccountsController> logger)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            //BaseUrl = appSettings.BaseUrl;
            //this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        // GET: UserAccounts
        public async Task<IActionResult> Index()
        {
            try
            {
                var adminUser = session?.GetCurrentAdmin();
                var users = new List<UserAccount>();

                if (TempData["Message"] != null)
                    ViewBag.Message = TempData["Message"];

                if (adminUser == null)
                {
                    users = new List<UserAccount>();
                }
                else if (adminUser.UserType == UserType.Administrator)
                {
                    users = await context.UserAccounts
                        .OrderBy(o => o.LastName)
                        .ToListAsync();
                }
                else if (adminUser.UserType == Enumerators.UserType.HMISAnalyst)
                {
                    users = await context.UserAccounts
                        .Where(u => u.UserType != Enumerators.UserType.Administrator && u.UserType != Enumerators.UserType.HMISAnalyst)
                        .OrderBy(o => o.LastName)
                        .ToListAsync();
                }
                else if (adminUser.UserType == Enumerators.UserType.FacilityChampion)
                {
                    users = await context.UserAccounts
                        .Where(u => u.UserType == Enumerators.UserType.GeneralUser && u.FacilityID == adminUser.FacilityID)
                        .OrderBy(o => o.LastName)
                        .ToListAsync();
                }

                ViewBag.ActiveUsers = users.Where(m => m.AccountStatus == Enumerators.RowStatus.Active).ToList();
                ViewBag.InactiveUsers = users.Where(m => m.AccountStatus == Enumerators.RowStatus.Inactive).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                ModelState.AddModelError("", ex.Message.ToString());
            }

            return View();
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || context.UserAccounts == null)
            {
                return NotFound();
            }

            var userAccount = await context.UserAccounts
                .Include(u => u.Facilities)
                .FirstOrDefaultAsync(m => m.UserAccountID == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            UserRoleDto userRole = new UserRoleDto();

            try
            {
                var adminUser = (UserAccount)session?.GetCurrentAdmin();

                if (id == null)
                    return BadRequest();

                var userInDb = context.UserAccounts.Find(id);

                if (userInDb == null)
                    return NotFound();

                if (adminUser.UserType != UserType.Administrator && userInDb.UserType == UserType.Administrator)
                    return BadRequest();

                if (adminUser.UserType == UserType.HMISAnalyst && (userInDb.UserType == UserType.Administrator || userInDb.UserType == UserType.HMISAnalyst))
                    return BadRequest();

                if (adminUser.UserType == UserType.FacilityChampion && userInDb.UserType != UserType.GeneralUser)
                    return BadRequest();

                string decryptedPassword = string.Empty;

               EncryptionHelpers encryptionHelper = new EncryptionHelpers();
                userInDb.Password = encryptionHelper.Decrypt(userInDb.Password);

                userRole.UserID = userInDb.UserAccountID;
                userRole.Name = userInDb.FirstName + " " + userInDb.LastName;
                userRole.Username = userInDb.Username;
                userRole.UserType = userInDb.UserType;
                userRole.Password = userInDb.Password;
                userRole.ConfirmPassword = userInDb.Password;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message.ToString());
            }

            return View(userRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRoleDto userRole)
        {
            try
            {
                if (ModelState.IsValid)
                {  
                    var adminUser = (UserAccount)session?.GetCurrentAdmin();

                    var userInDb = context.UserAccounts.Find(userRole.UserID);

                    if (userInDb == null)
                        return NotFound();

                    string encryptedPassword = string.Empty;
                    EncryptionHelpers encryptionHelper = new EncryptionHelpers();
                    encryptedPassword = encryptionHelper.Encrypt(userRole.Password);

                    userInDb.UserType = userRole.UserType;
                    userInDb.ModifiedBy = adminUser.UserAccountID;
                    userInDb.Password = encryptedPassword;
                    //userInDb.ConfirmPassword = encryptedPassword;
                    userInDb.DateModified = DateTime.Now;
                    userInDb.SyncStatus = RowSyncStatus.NotSynced;

                    context.Entry(userInDb).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message.ToString());
            }

            return View(userRole);
        }

        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || context.UserAccounts == null)
            {
                return NotFound();
            }

            var userAccount = await context.UserAccounts
                .Include(u => u.Facilities)
                .FirstOrDefaultAsync(m => m.UserAccountID == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (context.UserAccounts == null)
            {
                return Problem("Entity set 'DataContext.UserAccounts'  is null.");
            }
            var userAccount = await context.UserAccounts.FindAsync(id);
            if (userAccount != null)
            {
                context.UserAccounts.Remove(userAccount);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void LoadAccessRights(Guid id)
        {
            try
            {
                var accessList = context.userAccesses.Where(u => u.UserAccountID == id).Include("UserAccount").ToList();

                ViewBag.UserAccountID = id;

                if (accessList.Count > 0)
                    ViewBag.UserAccount = accessList[0].UserAccount.FirstName + " " + accessList[0].UserAccount.LastName;

                foreach (var item in accessList)
                {
                    if (item.Module == (byte)UserAccessModule.Admissions)
                        ViewBag.Admissions = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.ChiefComplaints)
                        ViewBag.ChiefComplaints = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.Clients)
                        ViewBag.Clients = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.DeathCertificates)
                        ViewBag.DeathCertificates = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.DiabeticProfile)
                        ViewBag.DiabeticProfile = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.Diagnosis)
                        ViewBag.Diagnosis = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.Discharge)
                        ViewBag.Discharge = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.DoctorsNote)
                        ViewBag.DoctorsNote = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.InternationalReferral)
                        ViewBag.InternationalReferral = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.InterReferrals)
                        ViewBag.InterReferrals = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.MedicationPlan)
                        ViewBag.MedicationPlan = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.NursingCare)
                        ViewBag.NursingCare = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.Partograph)
                        ViewBag.Partograph = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.PatientExaminations)
                        ViewBag.PatientExaminations = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.PostSurgeries)
                        ViewBag.PostSurgeries = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.Referral)
                        ViewBag.Referral = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.Surgeries)
                        ViewBag.Surgeries = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.TreatmentPlan)
                        ViewBag.TreatmentPlan = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.Users)
                        ViewBag.Users = item.Module.ToString();

                    if (item.Module == (byte)UserAccessModule.Vitals)
                        ViewBag.Vitals = item.Module.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }

        private void DeleteAccessRights(Guid id)
        {
            try
            {
                var accessList = context.userAccesses.Where(u => u.UserAccountID == id).ToList();

                if (accessList.Count > 0)
                {
                    foreach (var item in accessList)
                        context.userAccesses.Remove(item);

                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult UserAccess(Guid id)
        {
            try
            {
                //if (id == null)
                //    return BadRequest();

                LoadAccessRights(id);
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UserAccess(UserAccess userAccess)
        {
            try
            {
                string selectedModules = Request.Form["selectedModule"];

                if (string.IsNullOrEmpty(selectedModules))
                    ModelState.AddModelError("", "No module selected for the user!");

                if (ModelState.IsValid)
                {
                    var modules = selectedModules.Split(',');

                    DeleteAccessRights(userAccess.UserAccountID);

                    foreach (var m in modules)
                    {
                        UserAccess obj = new UserAccess
                        {
                            UserAccessID = Guid.NewGuid(),
                            Module = byte.Parse(m),
                            UserAccountID = userAccess.UserAccountID,
                            SyncStatus = RowSyncStatus.NotSynced
                        };

                        context.userAccesses.Add(obj);
                    }

                    context.SaveChanges();

                    TempData["Message"] = "User access rights saved successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message.ToString());
            }

            return View(userAccess);
        }

        public ActionResult ActivateUserAccount(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = context.UserAccounts.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            user.AccountStatus = RowStatus.Active;
            user.IsAccountActive = true;
            //user.ConfirmPassword = user.Password;

            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DeactivateUserAccount(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = context.UserAccounts.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            user.AccountStatus = RowStatus.Inactive;
            user.IsAccountActive = false;
            //user.ConfirmPassword = user.Password;

            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool UserAccountExists(Guid id)
        {
            return (context.UserAccounts?.Any(e => e.UserAccountID == id)).GetValueOrDefault();
        }
    }
}