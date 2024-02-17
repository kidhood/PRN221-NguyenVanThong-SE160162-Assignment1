using System;
using System.Collections.Generic;

namespace InventoryManagementBO.Models
{
    public partial class Inventory
    {
        public Inventory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int? InventoryType { get; set; }
        public int? TotalQuantity { get; set; }
        public DateTime? Date { get; set; }
        public decimal TotalCost { get; set; }
        public int? SupplierId { get; set; }

        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
