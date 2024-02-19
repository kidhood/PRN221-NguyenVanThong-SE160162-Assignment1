using InventoryManagementBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementService.Interface
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetOrderDetailsByOrderId(int id);
        public bool RegisterOrderDetail(OrderDetail orderDetail);
        bool RegisterOrderDetail(List<OrderDetail> orderDetails);
    }
}
