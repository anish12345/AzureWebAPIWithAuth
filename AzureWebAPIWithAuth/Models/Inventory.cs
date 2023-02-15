using System.ComponentModel.DataAnnotations;

namespace AzureWebAPIWithAuth.Models
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string InventoryName { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
