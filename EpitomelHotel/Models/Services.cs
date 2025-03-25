using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace EpitomelHotel.Models
{
    public class Services
    {
        [Key]
        public int ServiceID { get; set; }

        

        [Required(ErrorMessage = "ServiceName required.")]
        public string ServiceName { get; set; }
        






    }
}
