using ProductsCRUD.Models.Products;
using ProductsCRUD.Repositories.Products;
using System.Collections.Generic;

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
