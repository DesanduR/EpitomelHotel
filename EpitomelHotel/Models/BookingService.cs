using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpitomelHotel.Models
{
    public class BookingService
    {
        [Key]
        public int BookingServiceID { get; set; }

        [Required(ErrorMessage = "ServiceCost is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "ServiceCost must be non-negative.")]
        public decimal ServiceCost { get; set; }

        // One BookingService belongs to one Room
        [Required]
        [ForeignKey("Room")]
        public int RoomID { get; set; }

        [Display(Name = "Room Number")]
        public virtual Rooms Room { get; set; }

        
        public virtual ICollection<Services> Services { get; set; }
    }
}
