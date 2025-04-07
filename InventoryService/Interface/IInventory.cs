using InventoryService.Models;
namespace InventoryService.Interface
{
    public interface IInventory
    {
        Task<IEnumerable<Inventory>> GetAllInventoryRequest();
        Task<IEnumerable<Inventory>> GetInventoryReqofDepartment(string departmentName);
        Task<IEnumerable<Inventory>> GetInventorybyItem(string itemName);
        Task<Inventory> GetInventorybyItemID(int itemID);
        Task<Inventory> CreateInventoryRequest(Inventory inventory);
        Task<Inventory> UpdateInventoryRequest(int inventoryId, Inventory inventory);
        Task RemoveInventoryRequest(int inventoryId);


    }
}
