using ProductsCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCRUD.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product newProduct);

        List<Product> GetProducts();
        Product GetProductById(int id);
        void UpdateProduct(Product updatedProduct);
        void DeleteProduct(int id);
    }
}
