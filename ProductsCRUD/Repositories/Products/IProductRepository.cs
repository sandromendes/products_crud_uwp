using ProductsCRUD.Models.Products;
using System.Collections.Generic;
using System.Linq;

namespace ProductsCRUD.Repositories.Products
{
    public interface IProductRepository
    {
        void AddProduct(Product newProduct);
        IQueryable<Product> GetQueryable();
        List<Product> GetProducts();
        Product GetProductById(string id);
        List<Product> GetProductsByFilter(IQueryable<Product> filter);
        void UpdateProduct(Product updatedProduct);
        void DeleteProduct(string id);
    }
}
