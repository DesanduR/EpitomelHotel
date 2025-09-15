using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpitomelHotel.Models
{
    public class BookingService
    {
        [Key]
        public int BookingServiceID { get; set; }

       
        public decimal ServiceCost { get; set; }

        // One BookingService belongs to one Room
        [Required]
        [ForeignKey("Room")]
        public int RoomID { get; set; }

        [Display(Name = "Room Number")]
        public virtual Rooms Room { get; set; }


        [ForeignKey("ServiceID"), Required]
        public int ServiceID { get; set; }

        [Display(Name = "Service")]

        public Services Service { get; set; }

    }
}
