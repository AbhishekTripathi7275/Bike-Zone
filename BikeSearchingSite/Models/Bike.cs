using System.ComponentModel.DataAnnotations;
using BikeSearchingSite.Extensions;

namespace BikeSearchingSite.Models
{
    public class Bike
    {
        public int Id { get; set; }

        public Make Make { get; set; }
        [RegularExpression("^[1-9]*$", ErrorMessage = "Select Make")]

        public int MakeID { get; set; }

        public Model Model { get; set; }
        [RegularExpression("^[1-9]*$", ErrorMessage = "Select Model")]

        public int ModelID { get; set; }


        [Required(ErrorMessage = "Provide Year")]
        [YearRangeTillDate(2000, ErrorMessage = "Invalid Year")]
        public int Year { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Provide Milage")]
        public int Mileage { get; set; }

        public string Features { get; set; } = string.Empty;

        [Required(ErrorMessage = "Provide Seller Name")]        
        public string SellerName { get; set; }

        [EmailAddress(ErrorMessage ="Invalid Email ID")]
        public string SellerEmail { get; set; }

        [Required(ErrorMessage = "Provide Seller Phone")]
        public string SellerPhone { get; set; }


        [Required(ErrorMessage = "Provide Selling Price")]
        public int Price { get; set; }

        [Required]
        [RegularExpression("^[A-Z]*$", ErrorMessage = "Select Currency")]
        public string Currency { get; set; }

        public string ImagePath { get; set; } = string.Empty;
    }
}
