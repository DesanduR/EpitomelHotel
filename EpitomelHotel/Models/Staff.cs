using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Staff
    {
        [Required(ErrorMessage = "StaffID required.")]
        public int StaffID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public int Phonenumber { get; set; }






    }
}
