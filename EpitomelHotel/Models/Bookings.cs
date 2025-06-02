using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EpitomelHotel.Areas.Identity.Data;

namespace EpitomelHotel.Models
{
    public class Bookings
    {
        [Key]
       
        public int BookingID { get; set; }

        // One Booking has many Rooms
        public virtual ICollection<Rooms> Rooms { get; set; }

        // One Booking has many Payments
        public virtual ICollection<Payments> Payments { get; set; }

        [Required(ErrorMessage = "CheckIN required.")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "CheckOUT required.")]
        [DataType(DataType.Date)]
        
        public DateTime CheckOut { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentStatus { get; set; }

        [Required]
        [ForeignKey("ApplUser")]
        public string ApplUserID { get; set; }

        [Display(Name = "ApplUser Name")]
        // Enable lazy loading
        public virtual ApplUser ApplUser { get; set; }
    }
}
