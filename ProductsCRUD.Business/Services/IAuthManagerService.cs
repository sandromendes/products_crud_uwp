using ProductsCRUD.Domain.Models.Users;

namespace ProductsCRUD.Business.Services
{
    public interface IAuthManagerService
    {
        bool HasPermission(User user);
        bool IsAuthenticatedUserOnSession();
    }
}