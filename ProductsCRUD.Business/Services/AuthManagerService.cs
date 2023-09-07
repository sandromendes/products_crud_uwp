using ProductsCRUD.Business.Services.Token;
using ProductsCRUD.Common.Util;
using ProductsCRUD.Domain.Models.Users;
using ProductsCRUD.Infra.Session;

namespace ProductsCRUD.Business.Services
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
            //TODO
            return false;
        }
    }
}
