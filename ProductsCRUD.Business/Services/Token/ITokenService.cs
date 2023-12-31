﻿namespace ProductsCRUD.Business.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(string username, string password);
        bool ValidateToken(string token);
    }
}