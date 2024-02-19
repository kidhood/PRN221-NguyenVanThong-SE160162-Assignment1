using InventoryManagementBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementRepository.Interface
{
    public interface IOrderRepository
    {
        List<Order>? GetOrderByPaging(int pageNumber);
        Order RegisterNewOrder(Order order);
        public bool RegisterOrder(Order order);
    }
}
