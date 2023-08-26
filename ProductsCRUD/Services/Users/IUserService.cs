using ProductsCRUD.Models.Users;
using System;
using System.Collections.Generic;

namespace ProductsCRUD.Services.Users
{
    public interface IUserService
    {
        void RegisterUser(User user);
        bool ValidateCredentials(string email, string password);
        User GetUserByEmail(string email);
        List<User> GetAllUsers();
        User GetUserById(string id);
        void RemoveUser(string userId);
        void UpdateUser(User user);
    }
}