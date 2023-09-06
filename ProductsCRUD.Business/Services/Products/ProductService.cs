using ProductsCRUD.Models.Products;
using ProductsCRUD.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUD.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task AddProduct(Product newProduct)
        {
            newProduct.Id = Guid.NewGuid().ToString();
            await productRepository.Add(newProduct);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await productRepository.GetAll();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await productRepository.Get(id);
        }

        public List<Product> GetProductsByFilter(ProductFilterRequest request)
        {
            var filter = productRepository.GetQueryable();

            if (request.ProductName != null && request.ProductName != string.Empty)
                filter = filter.Where(p => p.Name.Contains(request.ProductName));

            if (request.ProductMinValue != 0)
                filter = filter.Where(p => p.Price >= request.ProductMinValue);

            if (request.ProductMaxValue != 0)
                filter = filter.Where(p => p.Price <= request.ProductMaxValue);

            return productRepository.GetProductsByFilter(filter);
        }

        public async void UpdateProduct(Product updatedProduct)
        {
            await productRepository.Update(updatedProduct);
        }

        public async void DeleteProduct(string id)
        {
            await productRepository.Delete(id);
        }
    }
}
