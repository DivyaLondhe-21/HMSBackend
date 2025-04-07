namespace PaymentService.DTO
{
    public class BillDTO
    {
        public int BillId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }
        public string Service { get; set; }
        public string Unit { get; set; }
        public int PaymentId { get; set; }
    }
}
