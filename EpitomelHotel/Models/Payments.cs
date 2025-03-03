using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Payments
    {
        public int PaymentID { get; set; }
        public Decimal Price { get; set; }
        public DateTime PayementDate { get; set; }
        public string PaymentMethod { get; set; }
        public Decimal TotalAmount { get; set; }

        [ForeignKey("BookingID")]
        
        [Display(Name = "Payements")]
        public Bookings Bookings { get; set; }

        [ForeignKey("BookingServiceID")]
        
        [Display(Name = "Service Name")]
        public BookingService BookingService { get; set; }


    }
}
