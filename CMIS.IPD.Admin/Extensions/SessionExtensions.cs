using IPD.Domain.Entities;
using Newtonsoft.Json;

namespace IPD.Admin.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetCurrentFacilityCode(this ISession session, string facilityCode)
        {
            session.SetString(SessionKey.FacilityCode, facilityCode);
        }
        public static string? GetCurrentFacilityCode(this ISession session)
        {
            var key = session.GetString(SessionKey.FacilityCode);
            return key;
        }

        public static void SetCurrentAdmin(this ISession session, UserAccount currentAdmin)
        {
            session.SetObjectAsJson(SessionKey.CurrentAdmin, currentAdmin);
        }
        
        public static UserAccount? GetCurrentAdmin(this ISession session)
        {
            return session.GetObjectFromJson<UserAccount?>(SessionKey.CurrentAdmin);
        }

        //public static void SetAdminSession(this ISession session, RecoveryRequest currentAdmin)
        //{
        //    session.SetObjectAsJson(SessionKey.CurrentAdmin, currentAdmin);
        //}

        //public static RecoveryRequest? GetAdminSession(this ISession session)
        //{
        //    return session.GetObjectFromJson<RecoveryRequest?>(SessionKey.CurrentAdmin);
        //}
    }

    internal static class SessionKey
    {
        public static readonly string FacilityCode = nameof(FacilityCode);
        public static readonly string CurrentAdmin = nameof(CurrentAdmin);
    }
}
