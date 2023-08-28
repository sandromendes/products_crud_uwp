using ProductsCRUD.Exceptions;
using ProductsCRUD.Infra.Session;
using ProductsCRUD.Models.Users;
using ProductsCRUD.Repositories.Users;
using ProductsCRUD.Services.Token;
using ProductsCRUD.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProductsCRUD.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;
        private readonly SessionCache sessionCache;

        public UserService(IUserRepository userRepository, 
            ITokenService tokenService,
            SessionCache sessionCache)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
            this.sessionCache = sessionCache;
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }
        public User GetUserById(string id)
        {
            return userRepository.GetUserById(id);
        }

        public User GetUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email);
        }

        public bool Exists(Expression<Func<User, bool>> predicate)
        {
            return userRepository.Exists(predicate);
        }

        public void RegisterUser(User user)
        {
            userRepository.AddUser(user);
        }

        public void RemoveUser(string userId)
        {
            userRepository.RemoveUser(userId);
        }

        public void UpdateUser(User user)
        {
            userRepository.UpdateUser(user);
        }

        public bool TryLogin(string email, string password, out bool isSuccess)
        {
            var userDb = userRepository.GetUserByEmail(email);
            if (userDb != null)
            {
                var passwordEncrypted = EncryptionUtils.Encrypt(password);

                if(passwordEncrypted != userDb.PasswordHash)
                {
                    isSuccess = false;
                    return false;
                }
                else
                {
                    var token = tokenService.CreateToken(userDb.FirstName, userDb.PasswordHash);
                    sessionCache.Set(AppConstants.TOKEN_KEY, token);
                    isSuccess = true;
                    return true;
                }
            }

            isSuccess = false;
            return false;
        }

        public void Logout()
        {
            if (sessionCache.Contains(AppConstants.TOKEN_KEY))
                sessionCache.Remove(AppConstants.TOKEN_KEY);
            else
                throw new UserNotLoggedException();
        }

        public void CreateSuperUserIfDoesntExists()
        {
            if(!Exists(a => a.FirstName == "admin"))
            {
                var user = new User
                {
                    CPF = string.Empty,
                    Email = "admin@admin",
                    FirstName = "admin",
                    LastName = "admin",
                    PasswordHash = EncryptionUtils.Encrypt("superuser"),
                    PhoneNumber = string.Empty
                };

                RegisterUser(user);
            }
        }
    }
}
