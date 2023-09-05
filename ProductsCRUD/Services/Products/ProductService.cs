using ProductsCRUD.Models.Products;
using ProductsCRUD.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public IQueryable<Product> GetQueryable()
        {
            return productRepository.GetQueryable();
        }

        public List<Product> GetProducts()
        {
            return productRepository.GetProducts();
        }

        public Product GetProductById(string id)
        {
            return productRepository.GetProductById(id);
        }

        public List<Product> GetProductsByFilter(IQueryable<Product> filter)
        {
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
