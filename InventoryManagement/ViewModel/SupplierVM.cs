using InventoryManagementBO.Models;
using InventoryManagementService;
using InventoryManagementService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementGUI.ViewModel
{
    class SupplierVM : Utilities.ViewModelBase
    {
        private readonly ISupplierService supplierService;
        private List<Supplier> suppliers;

        public SupplierVM()
        {
            supplierService = new SupplierService();
            suppliers = supplierService.GetSupplierByPaging(1);
        }

        public List<Supplier> SupplierDataGrid
        {
            get
            {
                return this.suppliers;
            }
        }
        
    }
}
