using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeSearchingSite.Models
{
    public class Model
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = null!;
        public Make Make { get; set; } = null!;

        [ForeignKey("Make")]
        public int MakeId { get; set; }
    }
}
