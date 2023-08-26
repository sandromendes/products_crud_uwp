using ProductsCRUD.Models.Users;
using System;
using System.Collections.Generic;

namespace ProductsCRUD.Repositories.Users
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserByEmail(string email);
        User GetUserByCompositeKey(User user);
        List<User> GetAllUsers();
        User GetUserById(string id);
        void RemoveUser(string userId);
        void UpdateUser(User user);
    }
}