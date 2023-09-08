using ProductsCRUD.Domain.Models.Categories;
using ProductsCRUD.Domain.Repositories.Categories;
using ProductsCRUD.Infra.DbContext;
using SQLite;
using System.Threading.Tasks;

namespace ProductsCRUD.Infra.Repositories.Categories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        private readonly SQLiteAsyncConnection databaseAsync;
        private readonly SQLiteConnection database;

        public ProductCategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            databaseAsync = appDbContext.AsyncConnection;
            database = appDbContext.Connection;
            Task.Run(() => databaseAsync.CreateTableAsync <ProductCategory>()).Wait();
        }
    }
}
