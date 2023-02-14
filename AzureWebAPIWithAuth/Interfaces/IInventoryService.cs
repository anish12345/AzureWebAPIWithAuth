using AzureWebAPIWithAuth.Models;

namespace AzureWebAPIWithAuth.Interfaces
{
    public interface IInventoryService
    {
        public Task<List<Inventory>> GetInventoryList();
        public Task<int> AddInventory(Inventory inventory);

        public Task<int> UpdateInventory(Inventory inventory);

        public Task<int> DeleteInventory(int InventoryID);
    }
}
