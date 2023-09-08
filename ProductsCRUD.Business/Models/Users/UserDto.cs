namespace ProductsCRUD.Business.Models.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }

        public UserDto()
        {
            
        }

        public UserDto(string firstName)
        {
            FirstName = firstName;
        }
    }
}
