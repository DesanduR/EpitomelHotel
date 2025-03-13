using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Payments
    {
        [Key]
        public int PaymentID { get; set; }

        
        public decimal Price { get; set; }
        public DateTime PayementDate { get; set; }

        [Required(ErrorMessage = "Payment Method required.")]
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }

        [ForeignKey("BookingID"), Required]
        public int BookingID { get; set; }
        [Display(Name = "Payements")]
        public Bookings Bookings { get; set; }

       
        

    }
}
