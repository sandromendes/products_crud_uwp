using ProductsCRUD.Business.Models.Products;
using ProductsCRUD.Domain.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCRUD.Business.Services.Products
{
    public interface IProductService
    {
        Task AddProduct(ProductDto newProductDto);
        Task<List<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(string id);
        List<ProductDto> GetProductsByFilter(ProductFilterRequest request);
        Task UpdateProduct(ProductDto updatedProductDto);
        Task DeleteProduct(string id);
    }
}
