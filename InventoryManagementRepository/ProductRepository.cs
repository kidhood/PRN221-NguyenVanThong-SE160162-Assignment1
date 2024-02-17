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
    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {
        }

        public List<Product> GetProductByName(string name)
        => ProductDAO.Instance.getProductsByName(name);

        public List<Product> GetProductByPaging(int pageNumber)
            =>  ProductDAO.Instance.getProductsByPaging(pageNumber);
        
    }
}
