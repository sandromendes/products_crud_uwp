using ProductsCRUD.Models.Users;

namespace ProductsCRUD.Services
{
    public interface IAuthManagerService
    {
        bool HasPermission(User user);
        bool IsAuthenticatedUserOnSession();
    }
}