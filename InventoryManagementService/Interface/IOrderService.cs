using InventoryManagementBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementService.Interface
{
    public interface IOrderService
    {
        Order RegisterNewOrder(Order order);
        public bool RegisterOrder(Order order);
    }
}
