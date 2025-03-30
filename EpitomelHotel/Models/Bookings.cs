using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EpitomelHotel.Areas.Identity.Data;

namespace EpitomelHotel.Models
{
    public class Bookings
    {
        [Key]
        public int BookingID { get; set; }

        public ICollection<Payments> Payments { get; set; }
        public ICollection<Rooms> Rooms { get; set; }

        [Required(ErrorMessage = "CheckIN required.")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "CheckOUT required.")]
        public DateTime CheckOut { get; set; }

      
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }

        [ForeignKey("ApplUser"), Required]
        public string ApplUserID { get; set; }
        
        [Display(Name = "ApplUser Name")]
        public ApplUser ApplUser { get; set; }


       
     

       




    }
}
