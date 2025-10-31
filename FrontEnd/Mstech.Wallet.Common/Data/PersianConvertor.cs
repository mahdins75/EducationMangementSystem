using System.Globalization;

namespace Common.Persian
{
    public static class PersianConvertor
    {
        public static string ToPersianInfo(this DateTime dateTime)
        {
            if (dateTime > DateTime.Now)
                return "";

            var totalDays = (DateTime.Now - dateTime).TotalDays;

            if (totalDays < 1)
            {
                var totalHours = (DateTime.Now - dateTime).TotalHours;
                if (totalHours < 1)
                {
                    var totalMin = (DateTime.Now - dateTime).TotalMinutes;
                    if (totalMin < 1)
                    {
                        return "چند لحظه";
                    }

                    return (int)totalMin + " دقیقه";
                }
                return (int)totalHours + " ساعت";
            }

            if (totalDays < 7)
                return (int)totalDays + " روز";

            if (totalDays < 30)
                return (int)(totalDays / 7) + " هفته";

            if ((DateTime.Now - dateTime).TotalDays < 365)
                return (int)(totalDays / 30) + " ماه";

            return (int)(totalDays / 365) + " سال";
        }
    }
}
