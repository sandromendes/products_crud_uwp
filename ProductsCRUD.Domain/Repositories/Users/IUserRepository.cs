using ProductsCRUD.Domain.Models.Users;
using System.Threading.Tasks;

namespace ProductsCRUD.Domain.Repositories.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        new Task Add(User user);
        Task<User> Get(User user);
        Task<User> GetUserByEmail(string email);
        Task<bool> Exists(User user);
        Task<User> GetUserByCompositeKey(User user);
    }
}