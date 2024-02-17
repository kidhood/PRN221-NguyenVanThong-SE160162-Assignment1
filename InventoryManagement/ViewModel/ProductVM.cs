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
   class ProductVM : Utilities.ViewModelBase
   {
        private readonly IProductService productService;
        private ObservableCollection<Product> products;
        private int productInCart;
        public ProductVM()
        {
            this.productService = new ProductService();
            products = new ObservableCollection<Product>(productService.GetProductByPaging(1));
        }

        public ObservableCollection<Product> ProductDataGrid
        {
            get
            {
                return this.products;
            }
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        public int ProductInCart
        {
            get { return productInCart; }
            set { productInCart = value; OnPropertyChanged(); }
        }
    }
}
