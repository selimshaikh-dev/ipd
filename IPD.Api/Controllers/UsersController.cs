using IPD.Api.Healper;
using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUnitOfWork unitOfWork, ILogger<UsersController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpPost]
        [Route("SaveOrUpdateUser")]
        public IActionResult SaveOrUpdateUser([FromBody] UserAccount userAccount)
        {
            try
            {
                if (userAccount.UserAccountID == Guid.Empty)
                {
                    EncryptionHelpers encryptionHelper = new EncryptionHelpers();
                    string encryptedPassword = string.Empty;
                    encryptedPassword = encryptionHelper.Encrypt(userAccount.Password);
                    userAccount.AccountStatus = RowStatus.Active;
                    userAccount.UserType = UserType.GeneralUser;
                    userAccount.Password = encryptedPassword;
                    var userAccountAdd = unitOfWork.UserAccountRepository.Add(userAccount);
                    unitOfWork.SaveChanges();
                    return Ok(userAccountAdd);
                }
                else
                {
                    var userinDb = unitOfWork.UserAccountRepository.GetById(userAccount.UserAccountID);
                    if (userinDb == null)
                    {
                        return NotFound();
                    }
                    userinDb.NationalID = userAccount.NationalID;
                    userinDb.FirstName = userAccount.FirstName;
                    userinDb.MiddleName = userAccount.MiddleName;
                    userinDb.LastName = userAccount.LastName;
                    userinDb.DOB = userAccount.DOB;
                    userinDb.Sex = userAccount.Sex;
                    userinDb.CellphoneCountryCode = userAccount.CellphoneCountryCode;
                    userinDb.Cellphone = userAccount.Cellphone;
                    userinDb.LandPhoneCountryCode = userAccount.LandPhoneCountryCode;
                    userinDb.LandPhone = userAccount.LandPhone;
                    userinDb.Email = userAccount.Email;
                    userinDb.ContactAddress = userAccount.ContactAddress;
                    userinDb.IsAdministrator = userinDb.IsAdministrator;
                    userinDb.FacilityID= userAccount.FacilityID;
                    userinDb.IsAccountActive = true;
                    var userAccountUp = unitOfWork.UserAccountRepository.Update(userinDb);
                    unitOfWork.SaveChanges();
                    return Ok(userAccountUp);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin(LoginDto login)
        {
            try
            {
                var user = unitOfWork.UserAccountRepository.GetUserByuserName(login.UserName);

                if (user != null)
                {
                    EncryptionHelpers encryptionHelper = new EncryptionHelpers();
                    string decryptedPassword = string.Empty;
                    decryptedPassword = encryptionHelper.Decrypt(user.Password);
                    if (decryptedPassword == login.Password)
                    {
                        var userAccesses = await unitOfWork.UserAccountRepository.GetUserAccessesAsync(user.UserAccountID);
                        user.UserAccess = userAccesses;
                        return Ok(user);
                    }
                    else
                    {
                        logger.LogError("Password not matched.");
                        return BadRequest("Password not matched.");
                    }
                }
                else
                {
                    logger.LogError("User Name not matched.");
                    return BadRequest("User Name not matched.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Save Recovery Request
        /// </summary>
        /// <param name="recoveryDtO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveRecoveryRequest")]
        public IActionResult SaveRecoveryRequest([FromBody] RecoveryRequestDto recoveryDtO)
        {
            try
            {
                var Check = unitOfWork.UserAccountRepository.GetbyCellPhoneOrUserId(recoveryDtO.CellPhone, recoveryDtO.UserName, recoveryDtO.NationaliD);
                if (Check != null)
                {
                    var userRecoveryTable = new RecoveryRequest()
                    {
                        CellphoneCountryCode = Check.CellphoneCountryCode,
                        Cellphone = Check.Cellphone,
                        Username = Check.Username,
                        NationalID = Check.NationalID,
                        DateRequested = DateTime.Now,
                        IsTicketOpen = true,
                        UserAccountID = Check.UserAccountID,
                    };
                    unitOfWork.RecoveryRequestRepository.Add(userRecoveryTable);
                    unitOfWork.SaveChanges();
                    userRecoveryTable.UserAccounts = null;
                    return Ok(userRecoveryTable);
                }
                else
                {
                    return NotFound("No matching account found !");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// CHange password
        /// </summary>
        /// <param name="changedPasswordDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveChangedPassword")]
        public IActionResult SaveChangedPassword([FromBody] ChangedPasswordDto changedPasswordDTO)
        {
            try
            {
                EncryptionHelpers encryptionHelper = new EncryptionHelpers();
                changedPasswordDTO.Password = encryptionHelper.Encrypt(changedPasswordDTO.Password);
                changedPasswordDTO.NewPassword = encryptionHelper.Encrypt(changedPasswordDTO.NewPassword);
                var Check = unitOfWork.UserAccountRepository.GetUserByuserNameAndpassword(changedPasswordDTO.UserName, changedPasswordDTO.Password);

                if (Check.Password == changedPasswordDTO.Password)
                {
                    if (Check != null)
                    {
                        Check.Password = changedPasswordDTO.NewPassword;
                        unitOfWork.UserAccountRepository.Update(Check);
                        unitOfWork.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return NotFound("No matching account found !");
                    }
                }
                else
                {

                   //  return RedirectToAction("ChangePassword", "Users", new { message = "not-match" });
                   return NotFound("Current password not match");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetForEdit")]
        public IActionResult GetForEdit(Guid UserAccountId)
        {
            try
            {
                var Result = unitOfWork.UserAccountRepository.FirstOrDefault(x => x.UserAccountID == UserAccountId);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllUserList")]
        public IActionResult GetAllUserList()
        {
            try
            {
                var list = unitOfWork.UserAccountRepository.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}