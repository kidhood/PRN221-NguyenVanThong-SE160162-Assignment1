using InventoryManagementBO.Models;
using InventoryManagementService;
using InventoryManagementService.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementGUI.ViewModel
{
    class ProductDetailDialog : Utilities.ViewModelBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;

        public ObservableCollection<KeyValuePair<string, string>> SupplierList { get; set; }

        public ProductDetailDialog()
        {
            _supplierService = new SupplierService();
            _productService = new ProductService();
            SupplierList = new ObservableCollection<KeyValuePair<string, string>>(_supplierService.GetSuppliers().Select(x => new KeyValuePair<string, string>(Convert.ToString(x.Id), x.Name)).ToList());
        }
    }
}
