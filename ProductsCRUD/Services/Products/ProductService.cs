using ProductsCRUD.Models.Products;
using ProductsCRUD.Repositories.Products;
using System.Collections.Generic;
using System.Linq;

namespace ProductsCRUD.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void AddProduct(Product newProduct)
        {
            productRepository.AddProduct(newProduct);
        }

        public List<Product> GetProducts()
        {
            return productRepository.GetProducts();
        }

        public Product GetProductById(string id)
        {
            return productRepository.GetProductById(id);
        }

        public List<Product> GetProductsByFilter(ProductFilterRequest request)
        {
            var filter = productRepository.GetQueryable();

            if (request.ProductName != null && request.ProductName != string.Empty)
                filter = filter.Where(p => p.Name == request.ProductName);

            if (request.ProductMinValue != 0)
                filter = filter.Where(p => p.Price >= request.ProductMinValue);

            if (request.ProductMaxValue != 0)
                filter = filter.Where(p => p.Price <= request.ProductMaxValue);

            return productRepository.GetProductsByFilter(filter);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            productRepository.UpdateProduct(updatedProduct);
        }

        public void DeleteProduct(string id)
        {
            productRepository.DeleteProduct(id);
        }
    }
}
