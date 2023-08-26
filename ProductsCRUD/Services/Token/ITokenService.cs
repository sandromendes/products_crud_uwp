namespace ProductsCRUD.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(string username, string password);
        bool ValidateToken(string token, string username, string password);
    }
}