using InventoryManagementBO.Models;
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
using System.Windows.Shapes;
using static InventoryManagementGUI.Dialog.CartDetail;

namespace InventoryManagementGUI.Dialog
{
    /// <summary>
    /// Interaction logic for CartDetail.xaml
    /// </summary>
    public partial class CartDetail : Window
    {
        private readonly IOrderService orderService;
        private readonly IOrderDetailService orderDetailService;
        private readonly IProductService productService;

        public ObservableCollection<CartItem> ListCartItem;
        public CartDetail(List<Product> listProductItem, bool isShowDetail = false)
        {
            InitializeComponent();
            orderService = new OrderService();
            orderDetailService = new OrderDetailService();
            productService = new ProductService();  
            int id = 0;
            List<CartItem> listCartItem = listProductItem.Select(x => new CartItem
            {
                Id = id++,
                ProductId = x.Id,
                Name = x.Name,
                Price = x.Price.Value,
                Quantity = x.Quantity.Value,
                TotalPrice = Math.Round(x.Price.Value * x.Quantity.Value, 2)
            }).ToList();

            ListCartItem = new ObservableCollection<CartItem>(listCartItem);
            cartDataGrid.ItemsSource = ListCartItem;

            txtTotalProduct.Text = listCartItem.Count.ToString();
            txtTotalPrice.Text = (Math.Round(ListCartItem.Sum(x => x.TotalPrice), 2)) + " $";

            if(isShowDetail)
            {
                btnOrder.Visibility = Visibility.Hidden;
                operatorColumn.Visibility = Visibility.Hidden;
            }

            DataContext = this;
        }

        private void btn_Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btn_Order(object sender, RoutedEventArgs e)
        {
            Order order = this.CreateOrder();
            Order orderTemp = orderService.RegisterNewOrder(order);
            if(orderTemp != null)
            {
                List<OrderDetail> orderDetails = this.CreateOrderDetail(orderTemp);
                if (orderDetailService.RegisterOrderDetail(orderDetails))
                {
                    MessageBox.Show("Order successfully");
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Order fail");
                }
            }
        }

        private Order CreateOrder()
        {
            Order order = new Order();
            order.TotalCost = Math.Round(ListCartItem.Sum(x => x.TotalPrice), 2);
            order.OrderDate = DateTime.Now;
            order.AccountId = 1;

            return order;
        }

        private List<OrderDetail> CreateOrderDetail(Order order)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach(var item in ListCartItem)
            {
                OrderDetail detail = new OrderDetail();
                detail.ProductId = item.ProductId;
                detail.Quantiy = item.Quantity;
                detail.OrderId = order.Id;
                detail.Price = item.Price;

                orderDetails.Add(detail);
            }

            return orderDetails;
        }

        private void btn_RemoveCartItem(object sender, RoutedEventArgs e)
        {
            var cartItemSelect = (CartItem)cartDataGrid.SelectedItem;
            ListCartItem.Remove(ListCartItem.First(x => x.Id == cartItemSelect.Id));
            cartDataGrid.ItemsSource = ListCartItem;
        }

        public class CartItem
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public double TotalPrice { get; set; }
        }


    }
}
