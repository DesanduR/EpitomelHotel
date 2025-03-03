using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class BookingService
    {
        public int BookingServiceID { get; set; }
        public string ServiceName { get; set; } 
        public Decimal ServiceCost { get; set; }

        [ForeignKey("ServiceID")]
        [Required(ErrorMessage = "Service Name required.")]
        [Display(Name = "Booking Service")]
        public Services Services { get; set; }

        [ForeignKey("GuestID")]
        [Required(ErrorMessage = "Guest Name required.")]
        [Display(Name = "GuestName")]
        public Guest Guest { get; set; }

        [ForeignKey("RoomID")]
        [Required(ErrorMessage = "Room Number required.")]
        [Display(Name = "Room Number")]
        public Rooms Rooms { get; set; }




    }
}
