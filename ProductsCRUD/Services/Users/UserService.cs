using ProductsCRUD.Exceptions;
using ProductsCRUD.Infra.Session;
using ProductsCRUD.Models.Users;
using ProductsCRUD.Repositories.Users;
using ProductsCRUD.Services.Token;
using ProductsCRUD.Util;
using System;
using System.Collections.Generic;

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

        public User GetUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email);
        }

        public User GetUserById(string id)
        {
            return userRepository.GetUserById(id);
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
    }
}
