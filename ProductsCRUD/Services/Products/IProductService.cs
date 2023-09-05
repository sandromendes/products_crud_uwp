using ProductsCRUD.Models.Products;
using System.Collections.Generic;

namespace ProductsCRUD.Services.Products
{
    public interface IProductService
    {
        void AddProduct(Product newProduct);
        List<Product> GetProducts();
        Product GetProductById(string id);
        List<Product> GetProductsByFilter(ProductFilterRequest request);
        void UpdateProduct(Product updatedProduct);
        void DeleteProduct(string id);
    }
}
