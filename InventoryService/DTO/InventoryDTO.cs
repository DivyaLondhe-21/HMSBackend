namespace InventoryService.DTO
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string DepartmentName { get; set; }
    }
}
