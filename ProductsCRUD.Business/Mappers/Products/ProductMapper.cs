using ProductsCRUD.Business.Models.Products;
using ProductsCRUD.Domain.Models.Products;
using System.Collections.Generic;
using System.Linq;

namespace ProductsCRUD.Business.Mappers.Products
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product product)
        {
            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ByteImage = product.Image
            };
        }

        public static Product ToEntity(ProductDto productDto)
        {
            if (productDto == null)
                return null;

            return new Product
            {
                Id = productDto.Id,
                Description = productDto.Description,
                Name = productDto.Name,
                Price = productDto.Price,
                Image = productDto.ByteImage
            };
        }

        public static IList<Product> ToEntityCollection(IList<ProductDto> productDtos)
        {
            var products = new List<Product>();
            if(productDtos != null && productDtos.Any())
                foreach (var productDto in productDtos)
                    products.Add(ToEntity(productDto));

            return products;
        }

        public static IList<ProductDto> ToDtoCollection(IList<Product> products) 
        {
            var dtos = new List<ProductDto>();
            if (products != null && products.Any())
                foreach (var product in products)
                    dtos.Add(ToDto(product));

            return dtos;
        }
    }
}
