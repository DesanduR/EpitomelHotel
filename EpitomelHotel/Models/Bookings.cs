using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpitomelHotel.Models
{
    public class Bookings
    {
        [Key]
        public int BookingID { get; set; }

        public ICollection<Payments> Payments { get; set; }

        [Required(ErrorMessage = "CheckIN required.")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "CheckOUT required.")]
        public DateTime CheckOut { get; set; }

      
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }

        [ForeignKey("GuestID"), Required]
        public int GuestID { get; set; }
        
        [Display(Name = "Guest Name")]
        public Guest Guest { get; set; }

        [ForeignKey("RoomID"), Required]
        public int RoomID { get; set; }
        
        [Display(Name = "Room Number")]
        public Rooms Rooms { get; set; }

        [ForeignKey("StaffID"), Required]
        public int StaffID { get; set; }
        [Display(Name = "StaffID")]
        public Staff Staff { get; set; }





    }
}
