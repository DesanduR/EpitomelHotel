using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


    }
}
