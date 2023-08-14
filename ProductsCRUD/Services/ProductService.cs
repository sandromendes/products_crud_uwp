using ProductsCRUD.Models;
using ProductsCRUD.Repositories;
using System.Collections.Generic;

namespace ProductsCRUD.Services
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

        public Product GetProductById(int id)
        {
            return productRepository.GetProductById(id);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            productRepository.UpdateProduct(updatedProduct);
        }

        public void DeleteProduct(int id)
        {
            productRepository.DeleteProduct(id);
        }
    }
}
