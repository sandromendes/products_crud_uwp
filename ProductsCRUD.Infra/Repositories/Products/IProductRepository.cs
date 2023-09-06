using ProductsCRUD.Models.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUD.Repositories.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        new Task Add(Product product);
        IQueryable<Product> GetQueryable();
        List<Product> GetProductsByFilter(IQueryable<Product> filter);
    }
}
