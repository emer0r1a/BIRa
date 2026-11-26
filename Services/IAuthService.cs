using BIRa.Models;

namespace BIRa.Services;

public record AuthResult(bool Succeeded, string? Error = null);

/// What the auth pages need from a backend. Swap MockAuthService for a real one in Program.cs.
public interface IAuthService
{
    Task<AuthResult> LoginAsync(LoginModel model);
    Task<AuthResult> RegisterAsync(RegisterModel model);
    Task<AuthResult> SendPasswordResetAsync(ForgotPasswordModel model);
}
