using BikeSearchingSite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeSearchingSite.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> Items)
        {

            List<SelectListItem> List = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem
            {
                Text = "----Select----",
                Value = "0"

            };
            List.Add(sli);

            foreach (var Item in Items)
            {
                sli = new SelectListItem
                {
                    Text = Item.GetPropertyValue("Name"),
                    Value = Item.GetPropertyValue("Id"),
                };
                List.Add(sli);
            }











            return List;

        }
    }
}
