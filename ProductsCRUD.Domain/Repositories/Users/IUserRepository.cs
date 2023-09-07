using ProductsCRUD.Domain.Models.Users;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductsCRUD.Domain.Repositories.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        new Task Add(User user);
        Task<User> Get(Expression<Func<User, bool>> predicate);
        Task<User> GetUserByEmail(string email);
        Task<bool> Exists(Expression<Func<User, bool>> predicate);
        Task<User> GetUserByCompositeKey(User user);
    }
}