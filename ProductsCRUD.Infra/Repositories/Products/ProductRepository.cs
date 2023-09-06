using ProductsCRUD.DbContext;
using ProductsCRUD.Models.Products;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUD.Repositories.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly SQLiteAsyncConnection databaseAsync;
        private readonly SQLiteConnection database;

        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            databaseAsync = appDbContext.AsyncConnection;
            database = appDbContext.Connection;
            databaseAsync.CreateTableAsync<Product>();
        }

        public async new Task Add(Product product)
        {
            var productDb = GetProductById(product.Id);
            if (productDb == null)
                await databaseAsync.InsertAsync(product);
        }

        public IQueryable<Product> GetQueryable()
        {
            return database.Table<Product>().AsQueryable();
        }

        public Product GetProductById(string id)
        {
            return database.Table<Product>().Where(a => a.Id == id).FirstOrDefault();
        }

        public List<Product> GetProductsByFilter(IQueryable<Product> query)
        {
            return query.ToList();
        }
    }
}
