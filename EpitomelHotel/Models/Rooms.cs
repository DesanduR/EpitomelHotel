using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpitomelHotel.Models
{
    public class Rooms
    {
        [Key]
        [Required(ErrorMessage = "RoomID required.")]
        public int RoomID { get; set; }

        // One Room has many BookingServices
        public virtual ICollection<BookingService> BookingServices { get; set; }

        [Required(ErrorMessage = "RoomType required.")]
        public string RoomType { get; set; }

        [Required(ErrorMessage = "Price required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Capacity required 1-5 only.")]
        [Range(1, 5)]
        public int Capacity { get; set; }

        [Required]
        public int StatusID { get; set; }

        [Display(Name = "Room Status")]
        public virtual Status Status { get; set; }

        [Required]
        public int StaffID { get; set; }

        [Display(Name = "StaffID")]
        public virtual Staff Staff { get; set; }

        [Required]
        public int BookingID { get; set; }

        public virtual Bookings Booking { get; set; }
    }
}
