using ProductsCRUD.Business.Services.Token;
using ProductsCRUD.Common.Exceptions;
using ProductsCRUD.Common.Util;
using ProductsCRUD.Domain.Models.Users;
using ProductsCRUD.Domain.Repositories.Users;
using ProductsCRUD.Infra.Session;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductsCRUD.Business.Services.Users
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

        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetAll();
        }

        public async Task<User> GetUserById(string id)
        {
            return await userRepository.Get(id);
        }

        public User GetUserByEmail(string email)
        {
            return Task.Run(() => userRepository.GetUserByEmail(email)).Result;
        }

        public User GetUser(Expression<Func<User, bool>> predicate)
        {
            return Task.Run(() => userRepository.Get(predicate)).Result;
        }

        public bool Exists(Expression<Func<User, bool>> predicate)
        {
            return Task.Run(() => userRepository.Exists(predicate)).Result;
        }

        public void RegisterUser(User user)
        {
            userRepository.Add(user);
        }

        public void RemoveUser(string userId)
        {
            userRepository.Delete(userId);
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }

        public bool TryLoginWithUserName(string userName, string password, out bool isSuccess)
        {
            var user = GetUser(u => u.FirstName == userName);

            return TryLogin(user.Email, password, out isSuccess);
        }

        public bool TryLogin(string email, string password, out bool isSuccess)
        {
            var userDb = Task.Run(() => userRepository.GetUserByEmail(email)).Result;
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
