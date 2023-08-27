using ProductsCRUD.Models.Users;
using System;
using System.Collections.Generic;

namespace ProductsCRUD.Services.Users
{
    public interface IUserService
    {
        void RegisterUser(User user);
        bool TryLogin(string email, string password, out bool isSuccess);
        void Logout();
        User GetUserByEmail(string email);
        List<User> GetAllUsers();
        User GetUserById(string id);
        void RemoveUser(string userId);
        void UpdateUser(User user);
    }
}