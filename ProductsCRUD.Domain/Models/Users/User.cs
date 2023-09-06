using SQLite;

namespace ProductsCRUD.Models.Users
{
    public class User : IEntity
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CPF { get; set; }
        public string PasswordHash { get; set; }
    }
}
