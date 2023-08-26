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

        public UserService(IUserRepository userRepository, 
            ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
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

        public bool ValidateCredentials(string email, string password)
        {
            var userDb = userRepository.GetUserByEmail(email);
            if (userDb != null)
            {
                var passwordEncrypted = EncryptionUtils.Encrypt(password);

                if(passwordEncrypted != userDb.PasswordHash) 
                    return false;
                else
                {

                    return true;
                }
            }
            return false;
        }
    }
}
