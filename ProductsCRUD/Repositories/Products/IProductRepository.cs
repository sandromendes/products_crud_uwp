using ProductsCRUD.Models.Products;
using System.Collections.Generic;

namespace ProductsCRUD.Repositories.Products
{
    public interface IProductRepository
    {
        void AddProduct(Product newProduct);
        List<Product> GetProducts();
        Product GetProductById(string id);
        void UpdateProduct(Product updatedProduct);
        void DeleteProduct(string id);
    }
}
