using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace EpitomelHotel.Models
{
    public class Services
    {
        public int ServiceID { get; set; }

        [ForeignKey("GuestID")]
        [Required(ErrorMessage = "Guest Name required.")]
        [Display(Name = "Guest Name")]
        public Guest Guest { get; set; }

    }
}
