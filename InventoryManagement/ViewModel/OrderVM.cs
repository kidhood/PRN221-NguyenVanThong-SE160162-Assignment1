using InventoryManagementBO.Models;
using InventoryManagementGUI.Utilities;
using InventoryManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementGUI.ViewModel
{
    class OrderVM : ViewModelBase
    {
        private readonly OrderService orderService;
        private List<Order> orders;

        public OrderVM()
        {
            orderService = new OrderService();
            orders = orderService.GetOrderByPaging(1);
        }

        public List<Order> OrdersDataGrid
        {
            get
            {
                return this.orders;
            }
        }


    }
}
