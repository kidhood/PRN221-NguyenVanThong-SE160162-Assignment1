using InventoryManagementBO.Models;
using InventoryManagementDAO;
using InventoryManagementRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementRepository
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {
        }

        public List<Order>? GetOrderByPaging(int pageNumber)
        => OrderDAO.Instance.GetOrderByPaging(pageNumber);

        public Order RegisterNewOrder(Order order)
        => OrderDAO.Instance.SaveNewOrder(order);

        public bool RegisterOrder(Order order)
         => OrderDAO.Instance.SaveOrder(order);
    }
}
