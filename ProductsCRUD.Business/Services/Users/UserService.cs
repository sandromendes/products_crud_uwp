using ProductsCRUD.Business.Mappers.Users;
using ProductsCRUD.Business.Models.Users;
using ProductsCRUD.Business.Services.Token;
using ProductsCRUD.Common.Exceptions;
using ProductsCRUD.Common.Util;
using ProductsCRUD.Domain.Repositories.Users;
using ProductsCRUD.Infra.Session;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<UserDto>> GetAllUsers()
        {
            return UserMapper.ToDtoCollection(await userRepository.GetAll()).ToList();
        }

        public async Task<UserDto> GetUserById(string id)
        {
            return UserMapper.ToDto(await userRepository.Get(id));
        }

        public UserDto GetUserByEmail(string email)
        {
            return UserMapper.ToDto(Task.Run(() => userRepository.GetUserByEmail(email)).Result);
        }

        public UserDto GetUser(UserDto dto)
        {
            var entity = UserMapper.ToEntity(dto);
            return UserMapper.ToDto(Task.Run(() => userRepository.Get(entity)).Result);
        }

        public bool Exists(UserDto dto)
        {
            var entity = UserMapper.ToEntity(dto);
            return Task.Run(() => userRepository.Exists(entity)).Result;
        }

        public void RegisterUser(UserDto user)
        {
            userRepository.Add(UserMapper.ToEntity(user));
        }

        public void RemoveUser(string userId)
        {
            userRepository.Delete(userId);
        }

        public void UpdateUser(UserDto user)
        {
            userRepository.Update(UserMapper.ToEntity(user));
        }

        public bool TryLoginWithUserName(string userName, string password, out bool isSuccess)
        {
            var user = GetUser(new UserDto(userName));

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
            if(!Exists(new UserDto("admin")))
            {
                var user = new UserDto
                {
                    CPF = string.Empty,
                    Email = "admin@admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = EncryptionUtils.Encrypt("superuser"),
                    PhoneNumber = string.Empty
                };

                RegisterUser(user);
            }
        }
    }
}
