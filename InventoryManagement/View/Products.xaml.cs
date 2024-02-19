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
    public partial class Products : UserControl
    {
        private readonly IProductService productService;
        private Dictionary<int ,Product> productIds = new Dictionary<int, Product>();
        public Products()
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
                if(productSelected.Quantity > product.Quantity)
                {
                    product.Quantity++;
                }
                else
                {
                    MessageBox.Show("Product out of stock");
                }
            }
            else
            {
                if(productSelected.Quantity > 0)
                {
                    var productTemp = new Product();
                    productTemp.Quantity = 1;
                    productTemp.Price = productSelected.Price;
                    productTemp.Id = productSelected.Id;
                    productTemp.Name = productSelected.Name;
                    productIds.Add(productSelected.Id, productTemp);
                }
                else
                {
                    MessageBox.Show("Product out of stock");
                }
            }

            txtNumberCart.Text = productIds.Values.Count().ToString();
        }

        private void btn_SearchByName(object sender, RoutedEventArgs e)
        {
            productDataGrid.ItemsSource = productService.GetProductByName(textBoxSearch.Text);
        }

        private void btn_ShowCartItem(object sender, RoutedEventArgs e)
        {
            CartDetail cartDetail = new CartDetail(this.productIds.Values.ToList());
            var isClose = cartDetail.ShowDialog();
            if(isClose != null && isClose.Value)
            {
                productIds = new Dictionary<int, Product>();
                txtNumberCart.Text = "0";
            }
            else
            {
            }

            productDataGrid.ItemsSource = productService.GetProductByPaging(1);
            txtNumberCart.Text = productIds.Values.Count().ToString();
        }
    }
}
