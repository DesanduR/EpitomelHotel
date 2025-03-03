using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpitomelHotel.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [ForeignKey("AddressID")]
        [Required(ErrorMessage = "Address required.")]
        [Display(Name = "Address")]
        public Address Address { get; set; }





    }
}
