using ProductsCRUD.Domain.Models.Categories;
using ProductsCRUD.Infra.DbContext;
using SQLite;

namespace ProductsCRUD.Infra.Repositories.Categories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        private readonly SQLiteAsyncConnection databaseAsync;
        private readonly SQLiteConnection database;

        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            databaseAsync = appDbContext.AsyncConnection;
            database = appDbContext.Connection;
            databaseAsync.CreateTableAsync <Category>();
        }
    }
}
