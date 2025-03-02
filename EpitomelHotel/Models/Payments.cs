namespace EpitomelHotel.Models
{
    public class Payments
    {
        public int PaymentID { get; set; }
        public Decimal Price { get; set; }
        public DateTime PayementDate { get; set; }
        public string PaymentMethod { get; set; }
        public Decimal TotalAmount { get; set; }



    }
}
