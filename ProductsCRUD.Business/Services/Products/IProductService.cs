using ProductsCRUD.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCRUD.Services.Products
{
    public interface IProductService
    {
        Task AddProduct(Product newProduct);
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(string id);
        List<Product> GetProductsByFilter(ProductFilterRequest request);
        void UpdateProduct(Product updatedProduct);
        void DeleteProduct(string id);
    }
}
