using InventoryManagementBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementDAO
{
    public class OrderDAO
    {
        private readonly InventoryManagementContext _dbContext = null;
        private static int PAGE_SIZE = 6;

        private static OrderDAO instance = null;

        public static OrderDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDAO();
                }
                return instance;
            }
        }

        public OrderDAO()
        {
            _dbContext = new InventoryManagementContext();
        }

        public bool SaveOrder(Order order)
        {
            try
            {
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Order SaveNewOrder(Order order)
        {
            try
            {
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();
                return order;
            }
            catch
            {
                return null;
            }
        }

        public List<Order>? GetOrderByPaging(int pageNumber)
        {
            return _dbContext.Orders
                    .OrderByDescending(p => p.OrderDate)
                    .Skip((pageNumber - 1) * PAGE_SIZE)
                    .Take(PAGE_SIZE)
                    .ToList();
        }
    }
}
