using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Staff
    {
        [Key]
        
        
        [Required(ErrorMessage = "StaffID required.")]
        public int StaffID { get; set; }
        
        public ICollection<Rooms> Rooms { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Profession { get; set; }
        public string Phonenumber { get; set; }






    }
}
