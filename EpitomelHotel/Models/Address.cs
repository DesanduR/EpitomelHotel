using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EpitomelHotel.Models
{
    public class Address
    {

        [Key]
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postalcode { get; set; }

        

    }
}
