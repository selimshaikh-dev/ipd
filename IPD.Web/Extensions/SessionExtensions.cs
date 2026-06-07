using IPD.Domain.Dto;
using IPD.Domain.Dto;
using Newtonsoft.Json;

namespace IPD.Web.Extensions
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

        public static void SetCurrentAdmissionId(this ISession session, string admissionId)
        {
            session.SetString(SessionKey.CurrentAdmissionId, admissionId);
        }

        public static string? GetCurrentAdmissionId(this ISession session)
        {
            return session.GetString(SessionKey.CurrentAdmissionId);
        }

        public static void SetCurrentClientId(this ISession session, string clientId)
        {
            session.SetString(SessionKey.CurrentClientId, clientId);
        }

        public static string? GetCurrentClientId(this ISession session)
        {
            return session.GetString(SessionKey.CurrentClientId);
        }

        public static UserRegistrationDto? GetCurrentUser(this ISession session)
        {
            return session.GetObjectFromJson<UserRegistrationDto?>(SessionKey.CurrentUser);
        }
        public static UsersDto? GetCurrentUsers(this ISession session)
        {
            return session.GetObjectFromJson<UsersDto?>(SessionKey.CurrentUser);
        }

        public static void SetCurrentUsers(this ISession session, UsersDto currentUser)
        {
            session.SetObjectAsJson(SessionKey.CurrentUser, currentUser);
        }

        public static void SetCurrentUser(this ISession session, UserRegistrationDto currentUser)
        {
            session.SetObjectAsJson(SessionKey.CurrentUser, currentUser);
        }
        public static void SetCurrentFacility(this ISession session, string facilityName)
        {
            session.SetString(SessionKey.FacilityName, facilityName);
        }
        public static string? GetCurrentFacility(this ISession session)
        {
            return session.GetString(SessionKey.FacilityName);
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
        public static string? GetCurrentFacilityID(this ISession session)
        {
            return session.GetString(SessionKey.FacilityID);
        }

        public static decimal CalculateBMI(Decimal weight, Decimal height)
        {
            try
            {
                if (weight > 0 && height > 0)
                {
                    decimal bmi = 0;
                    decimal heightInMeter = (height / 100);

                    //bmi = weight / (heightInMeter * heightInMeter);
                    bmi = weight / (heightInMeter * heightInMeter);
                  

                    return Decimal.Round(bmi, 2);
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }
        public static int CalculateClientsAge(DateTime DOB)
        {
            try
            {
                return DateTime.Now.Year - DOB.Year;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Determines adults’ nutritional status.
        /// </summary>
        /// <param name="bmi">Decimal</param>
        /// <returns>Nutritional status in string.</returns>
        public static string DetermineAdultsNutritionalStatus(decimal bmi, int age)
        {
            try
            {
                if (age > 18)
                {
                    if (bmi < 16)
                        return "Severely underweight";

                    if (bmi >= 30)
                        return "Obese";
                }
                else if (age <= 18)
                {
                    if (bmi < 11)
                        return "Severely underweight";

                    if (bmi >= 20)
                        return "Obese";
                }

                return "Normal";
            }
            catch
            {
                throw;
            }
        }
    }

    internal static class SessionKey
    {
        public static readonly string CurrentAdmissionId = nameof(CurrentAdmissionId);
        public static readonly string CurrentClientId = nameof(CurrentClientId);
        public static readonly string CurrentUser = nameof(CurrentUser);
        public static readonly string FacilityName = nameof(FacilityName);
        public static readonly string FacilityCode = nameof(FacilityCode);
        public static readonly string FacilityID = nameof(FacilityID);
    }
}
