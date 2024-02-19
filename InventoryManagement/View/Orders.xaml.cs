using InventoryManagementBO.Models;
using InventoryManagementGUI.Dialog;
using InventoryManagementService;
using InventoryManagementService.Interface;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        private readonly IProductService productService;
        private readonly IOrderDetailService orderDetailService;
       
        public Orders()
        {
            InitializeComponent();
            productService = new ProductService();
            orderDetailService = new OrderDetailService();
        }

        private void btn_ViewOrderDetail(object sender, RoutedEventArgs e)
        {
            var orderSelected = (Order)orderDataGrid.SelectedItem;
            List<OrderDetail> orderDetails = orderDetailService.GetOrderDetailsByOrderId(orderSelected.Id);

            var listProduct = new List<Product>();
            foreach (var orderDetail in orderDetails) {
                var product = orderDetail.Product;
                product.Price = orderDetail.Price;
                product.Quantity = orderDetail.Quantiy;
                listProduct.Add(product);
            }

            CartDetail cart = new CartDetail(listProduct, true);
            cart.ShowDialog();
        }
    }
}
