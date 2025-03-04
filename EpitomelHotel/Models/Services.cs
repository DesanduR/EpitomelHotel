using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace EpitomelHotel.Models
{
    public class Services
    {
        [Key]
        public int ServiceID { get; set; }

        [ForeignKey("GuestID")]
        public int GuestId { get; set; }
        [Display(Name = "Guest Name")]
        public Guest Guest { get; set; }

    }
}
