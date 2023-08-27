using ProductsCRUD.Infra.Session;
using ProductsCRUD.Models.Users;
using ProductsCRUD.Services.Token;
using ProductsCRUD.Util;

namespace ProductsCRUD.Services
{
    public class AuthManagerService : IAuthManagerService
    {
        private readonly ITokenService tokenService;
        private readonly SessionCache sessionCache;

        public AuthManagerService(ITokenService tokenService,
            SessionCache sessionCache)
        {
            this.tokenService = tokenService;
            this.sessionCache = sessionCache;
        }

        public bool IsAuthenticatedUserOnSession()
        {
            var authToken = sessionCache.Get<string>(AppConstants.TOKEN_KEY);

            return tokenService.ValidateToken(authToken);
        }

        public bool HasPermission(User user)
        {
            return false;
        }
    }
}
