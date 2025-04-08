using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Staff
    {
        [Key]
        
        
        [Required(ErrorMessage = "StaffID required.")]
        public int StaffID { get; set; }
        
        public ICollection<Rooms> Rooms { get; set; }
        [Required, MinLength(1), MaxLength(20), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Firstname required.")]
        public string Firstname { get; set; }
        [Required, MinLength(1), MaxLength(20), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Lastname required.")]
        public string Lastname { get; set; }
        [Required, MinLength(1), MaxLength(20), RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Profession required.")]
        public string Profession { get; set; }
        [Required, MinLength(1), MaxLength(20), RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Phonenumber required.")]
        public string Phonenumber { get; set; }






    }
}
