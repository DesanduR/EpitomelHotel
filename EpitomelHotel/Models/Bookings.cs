namespace EpitomelHotel.Models
{
    public class Bookings
    {
        public int BookingID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }


    }
}
