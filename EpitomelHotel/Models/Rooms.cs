using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class Rooms
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public Decimal Price { get; set; }
        public string Capacity { get; set; }

        [ForeignKey("StatusID")]
        
        [Display(Name = "Room Status")]
        public Status Status { get; set; }

    }
}
