using ProductsCRUD.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCRUD.Domain.Repositories
{
    public interface IGenericRepository<T> where T : IEntity, new()
    {
        Task<int> Add(T entity);
        Task<int> Delete(string id);
        Task<T> Get(string id);
        Task<List<T>> GetAll();
        Task<int> Update(T entity);
    }
}