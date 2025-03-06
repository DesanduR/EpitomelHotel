using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Status
    {
        [Key]

        [Required(ErrorMessage = "Status required.")]
        public int StatusID { get; set; }

        public ICollection<Rooms>Rooms { get; set; }
        public string StatusName { get; set; }

    }
}
