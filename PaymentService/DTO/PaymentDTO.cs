namespace PaymentService.DTO
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayTime { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardType { get; set; }
        public string Cvv { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CreditExpiryDate { get; set; }
        public int ReservationId { get; set; }
    }
}
