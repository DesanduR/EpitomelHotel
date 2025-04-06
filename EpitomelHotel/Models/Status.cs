using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Status
    {
        [Key]

        [Required(ErrorMessage = "Status required.")]
        public int StatusID { get; set; }

        public ICollection<Rooms>Rooms { get; set; }
        [Required, MinLength(1), MaxLength(20), RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "StatusName required.")]
        public string StatusName { get; set; }

    }
}
