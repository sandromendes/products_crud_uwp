using ProductsCRUD.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductsCRUD.Services.Users
{
    public interface IUserService
    {
        void RegisterUser(User user);
        bool TryLogin(string email, string password, out bool isSuccess);
        bool TryLoginWithUserName(string userName, string password, out bool isSuccess);
        bool Exists(Expression<Func<User, bool>> predicate);
        void Logout();
        User GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        User GetUser(Expression<Func<User, bool>> predicate);
        void RemoveUser(string userId);
        void UpdateUser(User user);
        void CreateSuperUserIfDoesntExists();
    }
}