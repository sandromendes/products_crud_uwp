using ProductsCRUD.DbContext;
using ProductsCRUD.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCRUD.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : IEntity, new()
    {
        private readonly SQLiteAsyncConnection database;

        public GenericRepository(AppDbContext context)
        {
            database = context.AsyncConnection;
        }

        public async Task<int> Add(T entity)
        {
            return await database.InsertAsync(entity);
        }

        public async Task<int> Delete(string id)
        {
            var register = await Get(id);
            if (register != null)
                return await database.DeleteAsync(register);

            return await Task.FromResult(0);
        }

        public async Task<T> Get(string id)
        {
            return await database.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAll()
        {
            return database.Table<T>().ToListAsync();
        }

        public async Task<int> Update(T entity)
        {
            var register = await Get(entity.Id);
            if (register != null)
                return await database.UpdateAsync(entity);

            return await Task.FromResult(0);
        }
    }
}
