using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeSearchingSite.Models.ViewModels
{
    public class ModelViewModel
    {
        public Model Model { get; set; } = null!;
        public IEnumerable<Make> Makes { get; set; } = null!;

        public IEnumerable<SelectListItem> CSelectListItem(IEnumerable<Make> Items)
        {

            List<SelectListItem> MakeList = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem
            {
                Text = "----Select----",
                Value = "0"

            };
            MakeList.Add(sli);

            foreach (Make make in Items)
            {
                sli = new SelectListItem
                {

                    Text = make.Name,
                    Value = make.Id.ToString(),
                };
                MakeList.Add(sli);
            }
            return MakeList;

        }
    }
}
