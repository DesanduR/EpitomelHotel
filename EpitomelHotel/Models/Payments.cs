using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpitomelHotel.Models
{
    public class Payments
    {
        [Key]
        public int PaymentID { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive.")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Payment Method required.")]
        public string PaymentMethod { get; set; }

        public decimal TotalAmount { get; set; }

        // Foreign key to Booking (one-to-many)
        [Required]
        [ForeignKey("Booking")]
        public int BookingID { get; set; }

        // Navigation property to Booking
        public virtual Bookings Booking { get; set; }
    }
}
