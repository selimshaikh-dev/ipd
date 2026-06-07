using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Web.Extensions;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace IPD.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly string BaseUrl;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<UsersController> logger;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public UsersController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory,ILogger<UsersController> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            BaseUrl = appSettings.BaseUrl;
            httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var facilities = new List<Facility>();
                using (var client = new HttpClient())
                {
                    var response2 = await client.GetAsync($"{BaseUrl}/Facilities/LoadFacilityName");
                    string result2 = response2.Content.ReadAsStringAsync().Result;
                    facilities = JsonConvert.DeserializeObject<List<Facility>>(result2);
                }

                ViewBag.facilities = facilities;
                var user = new UsersDto();
                user.BaseUrl = BaseUrl;
                return View(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsersDto userRegistration)
        {
            if (userRegistration.Sex == 0 || userRegistration.Sex == null)
            {
                userRegistration.Sex = userRegistration.Gender;
            }
           
            if (userRegistration.DOB == null)
            {
                userRegistration.DOB = userRegistration.dateOfBirth;
            }
            if (userRegistration.Password != userRegistration.ConfirmPassword)
            {
                var facilities = new List<Facility>();
                using (var client = new HttpClient())
                {
                    var response2 = await client.GetAsync($"{BaseUrl}/Facilities/LoadFacilityName");
                    string result2 = response2.Content.ReadAsStringAsync().Result;
                    facilities = JsonConvert.DeserializeObject<List<Facility>>(result2);
                }

                ViewBag.facilities = facilities;
                return View(userRegistration);
            }
            var userRegistrationJson = JsonConvert.SerializeObject(userRegistration);

            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(userRegistrationJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{BaseUrl}/Users/SaveOrUpdateUser", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    //List Show
                    var list = JsonConvert.DeserializeObject<PatientDto>(result);

                    return RedirectToAction("UserConfirmation", "Users", list);
                }
                else
                {
                    var facilities = new List<Facility>();
                    using (var client2 = new HttpClient())
                    {
                        var response2 = await client2.GetAsync($"{BaseUrl}/Facilities/LoadFacilityName");
                        string result2 = response2.Content.ReadAsStringAsync().Result;
                        facilities = JsonConvert.DeserializeObject<List<Facility>>(result2);
                    }
                    ViewBag.facilities = facilities;
                    return View(userRegistration);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var EditUser = new UserEditDto();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            Guid UserId = Guid.Empty;
            if (claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault() != null)
            {
                UserId = Guid.Parse(claim.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value?.ToString());
                EditUser.UserAccountID = UserId;
            }
            else
            {
                return BadRequest(new Exception("NotValidRequest"));
            }
            //var sex = new List<DropDown>();
            var facilities = new List<Facility>();
            using (var client = new HttpClient())
            {
                var response3 = await client.GetAsync($"{BaseUrl}/Users/GetForEdit?UserAccountId=" + EditUser.UserAccountID);
                var response2 = await client.GetAsync($"{BaseUrl}/Facilities/LoadFacilityName");
                string result2 = response3.Content.ReadAsStringAsync().Result;
                string result3 = response2.Content.ReadAsStringAsync().Result;
                EditUser = JsonConvert.DeserializeObject<UserEditDto>(result2);
                EditUser.Password = "mk3ljiuhytrfed==";
                facilities = JsonConvert.DeserializeObject<List<Facility>>(result3);
            }
            ViewBag.facilities = facilities;
            return View(EditUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditDto editUser)
        {
            var facilities = new List<Facility>();
            using (var client = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    var response2 = await client.GetAsync($"{BaseUrl}/Facilities/LoadFacilityName");
                    string result2 = response2.Content.ReadAsStringAsync().Result;
                    facilities = JsonConvert.DeserializeObject<List<Facility>>(result2);

                    ViewBag.facilities = facilities;
                    return View(editUser);
                }
                else
                {
                    var user = JsonConvert.SerializeObject(editUser);
                    HttpContent httpContent = new StringContent(user, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}/Users/SaveOrUpdateUser", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var userEdited = JsonConvert.DeserializeObject<UsersDto>(result);
                        var facilityResonse = await client.
                        GetAsync($"{BaseUrl}/Facilities/GetFacilityNameById?Facilities=" + editUser.FacilityID);
                        string facilityName = facilityResonse.Content.ReadAsStringAsync().Result;
                        if (userEdited != null)
                        {
                            session?.SetCurrentUsers(userEdited);
                            session?.SetCurrentFacility(facilityName);
                        }
                        return RedirectToAction("UserUpdateConfirmation", "Users");
                    }
                    else
                    {
                        var response3 = await client.GetAsync($"{BaseUrl}/Facilities/LoadFacilityName");
                        string result2 = response3.Content.ReadAsStringAsync().Result;
                        facilities = JsonConvert.DeserializeObject<List<Facility>>(result2);

                        ViewBag.facilities = facilities;
                        return View(editUser);
                    }
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string? model)
        {              
            var user = new ChangedPasswordDto();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            string UserId = string.Empty;
            if (claim.Where(x => x.Type == ClaimTypes.UserData).FirstOrDefault() != null)
            {
                UserId = claim.Where(x => x.Type == ClaimTypes.UserData).FirstOrDefault().Value?.ToString();
                user.UserName = UserId.ToString();
                ViewBag.message = model;
            }
            else
            {
                return BadRequest(new Exception("NotValidRequest"));
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangedPasswordDto changedPassword)
        {
            var userRegistrationJson = JsonConvert.SerializeObject(changedPassword);
            using (var client = new HttpClient())
            {
                var message = "";
                HttpContent httpContent = new StringContent(userRegistrationJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{BaseUrl}/Users/SaveChangedPassword", httpContent);
                if(response.ReasonPhrase == "Not Found")
                {
                    message = "not_found";
                    
                    return RedirectToAction("ChangePassword", "Users", new { model = message });
                }
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ChangePasswordConfirmation", "Users");
                }
            }
            return View(changedPassword);
        }

        public IActionResult UserConfirmation(UsersDto userRegistration)
        {
            return View(userRegistration);
        }

        public IActionResult UserUpdateConfirmation()
        {
           
            return RedirectToAction("profileSearch", "Clients");
        }

        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        
        public async Task<IActionResult> Index(string? message)
        {
            var Category = new List<Facility>();
            using (var client = new HttpClient())
            {

                var response = await client.GetAsync($"{BaseUrl}/Facilities/LoadFacilityName");
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Category = JsonConvert.DeserializeObject<List<Facility>>(result);
                }
            }
            ViewBag.facilities = Category;
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginDto users)
        {
            var loginJson = JsonConvert.SerializeObject(users);

            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(loginJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{BaseUrl}/Users/UserLogin", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var user = JsonConvert.DeserializeObject<UsersDto>(result);
                    if (user != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserAccountID.ToString()),
                            new Claim(ClaimTypes.UserData, user.UserName.ToString()),
                            new Claim(ClaimTypes.Role, "User"),
                        };
                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.Now.AddMinutes(10),
                        };
                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                        var facilityResonse = await client.
                        GetAsync($"{BaseUrl}/Facilities/GetFacilitiesById?Facilities=" + user.FacilityID);
                        string resultFacility = await facilityResonse.Content.ReadAsStringAsync();
                        var facility = JsonConvert.DeserializeObject<Facility>(resultFacility);
                        session?.SetCurrentUsers(user);
                        if (facility != null)
                        {
                            session?.SetCurrentFacility(facility.FacilityName);
                            session?.SetCurrentFacilityCode(facility.FacilityCode);
                        }
                        
                        return RedirectToAction("ProfileSearch", "Clients");
                    }
                    else
                    {
                        return RedirectToAction("Index", new {message ="notmatch"});
                    }
                }
                else
                {
                      return RedirectToAction("Index", new {message ="notmatch"});
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> NotAllow(string? message)
        {
            return View(message);
        }


    }
}