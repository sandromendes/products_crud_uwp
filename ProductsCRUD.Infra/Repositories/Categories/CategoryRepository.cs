using ProductsCRUD.DbContext;
using ProductsCRUD.Models.Categories;

namespace ProductsCRUD.Repositories.Categories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
