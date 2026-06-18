

namespace Cms.Application.Auth.DTOs;

public class LoginRequest
{

    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string CaotchaCode { get; set; } = default!;

}
