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
        


       

        [ForeignKey("RoomID"), Required]
        public int RoomID { get; set; }


        [Display(Name = "Room Number")]
        public Rooms Rooms { get; set; }



        




    }
}
