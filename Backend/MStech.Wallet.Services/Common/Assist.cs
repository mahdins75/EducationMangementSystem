using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.ComponentModel;
using System.Reflection;
namespace Implementation.CityService
{
    public static class AssistService
    {

        public static PersianCalendar pc = new PersianCalendar();
        public static string ConvertDateToPersianDate(DateTime dateTime)
        {
            try
            {
                return pc.GetYear(dateTime).ToString() + "/" + pc.GetMonth(dateTime).ToString() + "/" + pc.GetDayOfMonth(dateTime).ToString();

            }
            catch
            {

                return "";
            }

        }

        public static IEnumerable<SelectListItem> GetEnumAsSelectListItem<T>() where T : Enum
        {
            var result = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = null } };
            result.AddRange(Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(x => new SelectListItem
                       {
                           Text = GetEnumDescription(x),
                           Value = x.ToString()
                       }));
            return result;
        }
        private static string GetEnumDescription<T>(T value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute != null ? attribute.Description : value.ToString();
        }
        public static string GetEnumDescription(Enum value)
        {
            if (value == null)
            {
                return "";
            }
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
                return null;

            FieldInfo field = type.GetField(name);
            if (field == null)
                return null;

            DescriptionAttribute attribute =
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? name : attribute.Description;
        }
        public static DateTime ConvertPersianDateToGregourianDate(string dateTime)
        {
            string[] startDateParts = dateTime.Split('/');
            int year = int.Parse(startDateParts[0]);
            int month = int.Parse(startDateParts[1]);
            int day = int.Parse(startDateParts[2]);


            return pc.ToDateTime(year, month, day, 0, 0, 0, 0);

        }
    }

}