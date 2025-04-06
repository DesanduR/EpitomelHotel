using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace EpitomelHotel.Models
{
    public class Services
    {
        [Key]
        public int ServiceID { get; set; }

        

        [Required, MinLength(1), MaxLength(20), RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "ServiceName required.")]
        public string ServiceName { get; set; }

        [ForeignKey("BookingServiceID"), Required]
        public int BookingServiceID { get; set; }

        [Display(Name = "Booking Service")]

        public BookingService BookingService { get; set; }




    }
}
