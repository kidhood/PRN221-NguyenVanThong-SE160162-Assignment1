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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IProductRepository productRepository;
        public OrderDetailService()
        {
            orderDetailRepository = new OrderDetailRepository();
            productRepository = new ProductRepository();
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int id)
        => orderDetailRepository.GetOrderDetailsByOrderId(id);

        public bool RegisterOrderDetail(OrderDetail orderDetail)
        => orderDetailRepository.RegisterOrderDetail(orderDetail);

        public bool RegisterOrderDetail(List<OrderDetail> orderDetails)
        {

            List<Product> products = productRepository.GetProductByIds(orderDetails.Select(x => x.ProductId).ToList());
            foreach(var product  in products)
            {
                product.Quantity = product.Quantity - orderDetails.First(x => x.ProductId == product.Id).Quantiy;
            }

            if(productRepository.UpdateListProduct(products))
            {
                orderDetailRepository.RegisterOrderDetail(orderDetails);
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
