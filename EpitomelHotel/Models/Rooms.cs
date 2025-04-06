using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Rooms
    {
        [Key]
        [Required(ErrorMessage = "RoomID required.")]
        public int RoomID { get; set; }

        public ICollection<BookingService>BookingServices { get; set; }

        [Required(ErrorMessage = "RoomType required.")]
        public string RoomType { get; set; }
        [Required(ErrorMessage = "Price required.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Capacity required 1-5 only."), Range(1,5)]
        public int Capacity { get; set; }

        [ForeignKey("StatusID"), Required]
        public int StatusID { get; set; }
        [Display(Name = "Room Status")]
        public Status Status { get; set; }

        [ForeignKey("StaffID"), Required]
        public int StaffID { get; set; }
        [Display(Name = "StaffID")]
        public Staff Staff { get; set; }


        [ForeignKey("BookingID"), Required]
        public int BookingID { get; set; }
        [Display(Name = "Payements")]
        public Bookings Bookings { get; set; }
    }
}
