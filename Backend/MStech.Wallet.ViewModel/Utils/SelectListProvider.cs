using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mstech.ViewModel.Utils
{
    public static class SelectListProvider
    {
        public static IList<SelectListItem> AddEmptySelectItem(this IList<SelectListItem> items)
        {
            items.Insert(0, GetSelectItem());

            return items;
        }

        public static SelectListItem GetSelectItem(bool selected = false)
            => new SelectListItem
            {
                Selected = selected,
                Text = "انتخاب کنید",
                Value = ""
            };

    }
}
