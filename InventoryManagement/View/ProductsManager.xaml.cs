using InventoryManagementBO.Models;
using InventoryManagementGUI.Dialog;
using InventoryManagementService;
using InventoryManagementService.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagementGUI.View
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class ProductsManager : UserControl
    {
        private readonly IProductService productService;
        private Dictionary<int ,Product> productIds = new Dictionary<int, Product>();
        public ProductsManager()
        {
            InitializeComponent();
            productService = new ProductService();
        }

        private void btn_PagingClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int pageNumber = int.Parse(button.Content.ToString());
                productDataGrid.ItemsSource = productService.GetProductByPaging(pageNumber);
            }
        }

        private void btn_AddToCart(object sender, RoutedEventArgs e)
        {
            var productSelected = (Product) productDataGrid.SelectedItem;
            if(productIds.TryGetValue(productSelected.Id, out var product))
            {
                product.Quantity++;
            }
            else
            {
                productSelected.Quantity = 1;
                productIds.Add(productSelected.Id, productSelected);
            }

        }

        private void btn_SearchByName(object sender, RoutedEventArgs e)
        {
            productDataGrid.ItemsSource = productService.GetProductByName(textBoxSearch.Text);
        }

        private void btn_AddNewProduct(object sender, RoutedEventArgs e)
        {
            ProductDetail productDetail = new ProductDetail();
            productDetail.Show();
        }

        private void btn_UpdateProduct(object sender, RoutedEventArgs e)
        {
            var productSelected = (Product) productDataGrid.SelectedItem;
            ProductDetail productDetail = new ProductDetail(productSelected, true);
            var isShow = productDetail.ShowDialog();

            if (isShow != null && isShow.Value)
            {
                productDataGrid.ItemsSource = productService.GetProductByPaging(1);
            }
            else
            {
                productDataGrid.ItemsSource = productService.GetProductByPaging(1);
            }
        }

        private void btn_DeleteProduct(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this product?",
            "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var productSelected = (Product)productDataGrid.SelectedItem;
                productSelected.IsDelete = true;
                if(productService.UpdateProduct(productSelected))
                {
                    MessageBox.Show("Delete product successfully");
                    productDataGrid.ItemsSource = productService.GetProductByPaging(1);
                }
                else
                {
                    MessageBox.Show("Delete product fail");
                }
            }
        }
    }
}
