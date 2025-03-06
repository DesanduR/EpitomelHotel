using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Rooms
    {
        [Key]
        [Required(ErrorMessage = "RoomID required.")]
        public int RoomID { get; set; }

        [Required(ErrorMessage = "RoomType required.")]
        public string RoomType { get; set; }
        [Required(ErrorMessage = "Price required.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Capacity required.")]
        public string Capacity { get; set; }

        [ForeignKey("StatusID"), Required]
        public int StatusID { get; set; }
        [Display(Name = "Room Status")]
        public Status Status { get; set; }

    }
}
