using System.Globalization;

namespace Common.Persian
{
    public static class PersianDateTimeExts
    {
        public static bool IsValidPersianDate(this int persianYear, int persianMonth, int persianDay) {
            if (persianDay > 31 || persianDay <= 0) {
                return false;
            }

            if (persianMonth > 12 || persianMonth <= 0) {
                return false;
            }

            if (persianMonth <= 6 && persianDay > 31) {
                return false;
            }

            if (persianMonth >= 7 && persianDay > 30) {
                return false;
            }

            if (persianMonth == 12) {
                var persianCalendar = new PersianCalendar();
                var isLeapYear = persianCalendar.IsLeapYear(persianYear);

                if (isLeapYear && persianDay > 30) {
                    return false;
                }

                if (!isLeapYear && persianDay > 29) {
                    return false;
                }
            }

            return true;
        }

        public static DateTime ConvertShamsiToDateTime(this int persianYear, int persianMonth, int persianDay) 
            => new PersianCalendar().ToDateTime(persianYear, persianMonth, persianDay, 0, 0, 0, 0);


    }
}
