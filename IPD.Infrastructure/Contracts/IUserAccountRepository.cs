using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        UserAccount GetbyCellPhoneOrUserId(string Cellphone, string Username = "", string NationaliD = "");

        UserAccount? GetUserByuserNameAndpassword(string UserName, string Password);

        UserAccount? GetUserByuserName(string UserName);

        UserAccount IsLogin(int FacilitiesId, string userName, string password);

        Task<List<UserAccess>> GetUserAccessesAsync(Guid userAccountId);
    }
}
