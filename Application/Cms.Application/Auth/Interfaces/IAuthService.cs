

using Cms.Application.Auth.DTOs;

namespace Cms.Application.Auth.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Task<RegisterResponse?> RegisterAsync(RegisterRequest request);
    Task<LoginResponse?> RefreshToken(string refreshToken);
}
