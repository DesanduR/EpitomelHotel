using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class BookingService
    {

        [Key]
        public int BookingServiceID { get; set; }

        [Required(ErrorMessage = "Service Name required.")]
        public string ServiceName { get; set; } 
        public decimal ServiceCost { get; set; }

        [ForeignKey("ServiceID"), Required]
        public int ServiceID { get; set; }
        [Display(Name = "Booking Service")]
        public Services Services { get; set; }

        [ForeignKey("GuestID"), Required]
        public int GuestID { get; set; }
        [Display(Name = "GuestName")]
        public Guest Guest { get; set; }

        [ForeignKey("RoomID"), Required]
        public int RoomID { get; set; }


        [Display(Name = "Room Number")]
        public Rooms Rooms { get; set; }




    }
}
