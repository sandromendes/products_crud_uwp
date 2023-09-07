using ProductsCRUD.Business.Mappers.Products;
using ProductsCRUD.Business.Models.Products;
using ProductsCRUD.Domain.Models.Products;
using ProductsCRUD.Domain.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUD.Business.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task AddProduct(ProductDto newProductDto)
        {
            newProductDto.Id = Guid.NewGuid().ToString();

            await productRepository.Add(ProductMapper.ToEntity(newProductDto));
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            var products = await productRepository.GetAll();
            return ProductMapper.ToDtoCollection(products).ToList();
        }

        public async Task<ProductDto> GetProductById(string id)
        {
            return ProductMapper.ToDto(await productRepository.Get(id));
        }

        public List<ProductDto> GetProductsByFilter(ProductFilterRequest request)
        {
            var filter = productRepository.GetQueryable();

            if (request.ProductName != null && request.ProductName != string.Empty)
                filter = filter.Where(p => p.Name.Contains(request.ProductName));

            if (request.ProductMinValue != 0)
                filter = filter.Where(p => p.Price >= request.ProductMinValue);

            if (request.ProductMaxValue != 0)
                filter = filter.Where(p => p.Price <= request.ProductMaxValue);

            return ProductMapper.ToDtoCollection(productRepository.GetProductsByFilter(filter)).ToList();
        }

        public async Task UpdateProduct(ProductDto updatedProductDto)
        {
            await productRepository.Update(ProductMapper.ToEntity(updatedProductDto));
        }

        public async Task DeleteProduct(string id)
        {
            await productRepository.Delete(id);
        }
    }
}
