using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace EpitomelHotel.Models
{
    public class Services
    {
        [Key]
        public int ServiceID { get; set; }

        public ICollection<BookingService> BookingService { get; set; }



     

        
    }
}
