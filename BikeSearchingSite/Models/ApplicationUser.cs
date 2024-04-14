using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeSearchingSite.Models
{
    public class ApplicationUser:IdentityUser
    {
        [DisplayName("Office Phone")]
        public string PhoneNumber2 { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }
    }
}
