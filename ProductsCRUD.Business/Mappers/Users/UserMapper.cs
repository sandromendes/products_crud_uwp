using ProductsCRUD.Business.Models.Users;
using ProductsCRUD.Domain.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace ProductsCRUD.Business.Mappers.Users
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                CPF = user.CPF,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.PasswordHash,
                PhoneNumber = user.PhoneNumber
            };
        }

        public static User ToEntity(UserDto userDto)
        {
            if (userDto == null)
                return null;

            return new User
            {
                Id = userDto.Id,
                CPF = userDto.CPF,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PasswordHash = userDto.Password,
                PhoneNumber = userDto.PhoneNumber
            };
        }

        public static IList<User> ToEntityCollection(IList<UserDto> userDtos)
        {
            var users = new List<User>();
            if (userDtos != null && userDtos.Any())
                foreach (var userDto in userDtos)
                    users.Add(ToEntity(userDto));

            return users;
        }

        public static IList<UserDto> ToDtoCollection(IList<User> users)
        {
            var dtos = new List<UserDto>();
            if (users != null && users.Any())
                foreach (var user in users)
                    dtos.Add(ToDto(user));

            return dtos;
        }
    }
}
