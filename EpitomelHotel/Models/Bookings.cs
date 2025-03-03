using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpitomelHotel.Models
{
    public class Bookings
    {
        public int BookingID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }

        [ForeignKey("GuestID")]
        [Required(ErrorMessage = "Guest Name required.")]
        [Display(Name = "Guest Name")]
        public Guest Guest { get; set; }

        [ForeignKey("RoomID")]
        [Required(ErrorMessage = "Room Number required.")]
        [Display(Name = "Room Number")]
        public Rooms Rooms { get; set; }

        [ForeignKey("StaffID")]
        [Required(ErrorMessage = "StaffID required.")]
        [Display(Name = "StaffID")]
        public Staff Staff { get; set; }





    }
}
