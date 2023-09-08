using ProductsCRUD.Business.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductsCRUD.Business.Services.Users
{
    public interface IUserService
    {
        void RegisterUser(UserDto user);
        bool TryLogin(string email, string password, out bool isSuccess);
        bool TryLoginWithUserName(string userName, string password, out bool isSuccess);
        bool Exists(UserDto userDto);
        void Logout();
        UserDto GetUserByEmail(string email);
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(string id);
        UserDto GetUser(UserDto userDto);
        void RemoveUser(string userId);
        void UpdateUser(UserDto user);
        void CreateSuperUserIfDoesntExists();
    }
}