using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpitomelHotel.Models
{
    public class Guest
    {
        [Key]
        
        public int GuestId { get; set; }
        
        [Required(ErrorMessage = "First Name required.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last Name required.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number required.")]
        public string Phone { get; set; }

        [ForeignKey("AddressID"), Required]
        public int AddressID { get; set; }
        [Display(Name = "Address")]
        public Address Address { get; set; }





    }
}
