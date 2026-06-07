using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(DataContext context) : base(context)
        {
        }

        public UserAccount GetbyCellPhoneOrUserId(string Cellphone, string Username = "", string NationaliD = "")
        {
            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    var result = _context.UserAccounts.FirstOrDefault(x => x.Cellphone == Cellphone && x.NationalID == NationaliD);
                    return result;
                }
                else if (string.IsNullOrEmpty(NationaliD))
                {
                    var result = _context.UserAccounts.FirstOrDefault(x => x.Cellphone == Cellphone && x.Username == Username);
                    return result;
                }
                else if (string.IsNullOrEmpty(null))
                {
                    var result = _context.UserAccounts.FirstOrDefault(x => x.Cellphone == Cellphone && x.Username == Username && x.NationalID==NationaliD);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserAccount? GetUserByuserNameAndpassword(string UserName, string Password)
        {
            try
            {
                var user = _context.UserAccounts.AsNoTracking().FirstOrDefault(x => x.Username == UserName && x.Password == Password);
                if(user == null) {
                    user = new UserAccount();
                }
                return user ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserAccount? GetUserByuserName(string UserName)
        {
            try
            {
                var user = _context.UserAccounts.AsNoTracking().FirstOrDefault(x => x.Username == UserName);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserAccount IsLogin(int FacilitiesID, string userName, string password)
        {
            try
            {
                var user = _context.UserAccounts.AsNoTracking().FirstOrDefault(x => x.FacilityID == FacilitiesID &&
                     x.Username == userName && x.Password == password && x.IsAccountActive == true);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UserAccess>> GetUserAccessesAsync(Guid userAccountId)
        {
            var userAccess = await _context.UserAccounts
                .Include(x => x.UserAccess)
                .AsNoTracking()
                .Where(x => x.UserAccountID == userAccountId)
                .Select(x => x.UserAccess)
                .ToListAsync();

            return userAccess.SelectMany(x => x ?? new List<UserAccess>()).ToList();
        }
    }
}
