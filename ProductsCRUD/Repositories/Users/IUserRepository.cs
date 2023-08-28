using ProductsCRUD.Models.Users;
using System;
using System.Linq.Expressions;

namespace ProductsCRUD.Repositories.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        new void Add(User user);
        User GetUserByEmail(string email);
        bool Exists(Expression<Func<User, bool>> predicate);
        User GetUserByCompositeKey(User user);
    }
}