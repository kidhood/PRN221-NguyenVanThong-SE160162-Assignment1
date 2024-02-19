using InventoryManagementBO.Models;
using InventoryManagementRepository;
using InventoryManagementRepository.Interface;
using InventoryManagementService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService()
        {
            orderRepository = new OrderRepository();
        }

        public List<Order>? GetOrderByPaging(int pageNumber)
        => orderRepository.GetOrderByPaging(pageNumber);

        public Order RegisterNewOrder(Order order)
        => orderRepository.RegisterNewOrder(order);

        public bool RegisterOrder(Order order)
        => orderRepository.RegisterOrder(order);
    }
}
