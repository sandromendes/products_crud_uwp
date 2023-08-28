using ProductsCRUD.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProductsCRUD.Repositories.Users
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserById(string id);
        User GetUserByEmail(string email);
        bool Exists(Expression<Func<User, bool>> predicate);
        User GetUserByCompositeKey(User user);
        List<User> GetAllUsers();
        void RemoveUser(string userId);
        void UpdateUser(User user);
    }
}