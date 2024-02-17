using System;
using System.Collections.Generic;

namespace InventoryManagementBO.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
