using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EpitomelHotel.Models
{
    public class BookingService
    {

        [Key]
        public int BookingServiceID { get; set; }
        public ICollection<Services> Services { get; set; }
        

        [Required(ErrorMessage = "Service Name required.")]
        public decimal ServiceCost { get; set; }
        public string ServiceName { get; set; }


        [ForeignKey("ServiceID"), Required]
        public int ServiceID { get; set; }

        [Display(Name = "Booking Service")]

       

        [ForeignKey("RoomID"), Required]
        public int RoomID { get; set; }


        [Display(Name = "Room Number")]
        public Rooms Rooms { get; set; }



        




    }
}
