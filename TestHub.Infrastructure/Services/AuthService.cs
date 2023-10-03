using System.Security.Cryptography;
using Azure;
using Microsoft.AspNetCore.Http;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Services;

public class AuthService
{
    private readonly UserService _userService;

    public AuthService(UserService userService)
    {
        _userService = userService;
    }

    public RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(6)
        };

        return refreshToken;
    }

    public void SetRefreshToken(User user, RefreshToken newRefreshToken, HttpResponse httpResponse)
    {
        var cookieOption = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        
        httpResponse.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOption);

        user.RefreshToken = newRefreshToken.Token;
        user.TokenCreated = newRefreshToken.CreatedAt;
        user.TokenExpires = newRefreshToken.Expires;
        
        _userService.Update(user);
    }
}