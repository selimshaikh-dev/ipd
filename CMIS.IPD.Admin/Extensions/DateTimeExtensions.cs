namespace IPD.Admin.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsWithin24Hours(this DateTime dateTime, DateTime timeToCompare)
        {
            if (dateTime < timeToCompare)
            {
                return true;
            }

            var hours = (dateTime - timeToCompare).TotalHours;
            return hours <= 24;
        }

        public static (int, int, int) GetYearsMonthsDays(this DateTime dateTime, DateTime timeToCompare)
        {
            if (dateTime < timeToCompare)
            {
                return (0, 0, 0);
            }

            var zeroTime = new DateTime(1, 1, 1);
            var span = dateTime - timeToCompare;

            var years = (zeroTime + span).Year - 1;
            var months = (zeroTime + span).Month - 1;
            var days = (zeroTime + span).Day - 1;

            return (years, months, days);
        }

        public static string GetYearsMonthsDaysText(this DateTime dateTime, DateTime timeToCompare)
        {
            var (years, months, days) = dateTime.GetYearsMonthsDays(timeToCompare);
            var yearMonthDayText = string.Empty;

            if (years > 0)
            {
                yearMonthDayText += $"{years} year";
                if (years != 1)
                {
                    yearMonthDayText += "s";
                }
            }

            if (months > 0)
            {
                yearMonthDayText += $" {months} month";
                if (months != 1)
                {
                    yearMonthDayText += "s";
                }
            }

            if (days > 0)
            {
                yearMonthDayText += $" {days} day";
                if (days != 1)
                {
                    yearMonthDayText += "s";
                }
            }

            yearMonthDayText = yearMonthDayText.Trim();

            return yearMonthDayText;
        }
    }
}
