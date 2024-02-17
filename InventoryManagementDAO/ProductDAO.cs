using InventoryManagementBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementDAO
{
    public class ProductDAO
    {
        private readonly InventoryManagementContext _dbContext = null;
        private static int PAGE_SIZE = 6;

        private static ProductDAO instance = null;

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
        }

        public ProductDAO()
        {
            _dbContext = new InventoryManagementContext();
        }

        public List<Product> getProductsByPaging(int pageNumber)
        {
            return _dbContext.Products
                    .Where(p => !p.IsDelete.Value)
                    .OrderBy(p => p.Id)
                    .Skip((pageNumber - 1) * PAGE_SIZE)
                    .Take(PAGE_SIZE)
                    .ToList();
        }

        public List<Product> getProductsByName(string name)
        {
            return _dbContext.Products.Where(p => !p.IsDelete.Value && p.Name.ToLower().Contains(name)).ToList(); 
        }
    }
}
