using BIRa.Models;

namespace BIRa.Services;

/// Demo only: waits a moment and succeeds. Nothing is stored or sent.
public class MockAuthService : IAuthService
{
    private const int DelayMs = 900;

    public async Task<AuthResult> LoginAsync(LoginModel model)
    {
        await Task.Delay(DelayMs);
        return new AuthResult(true);
    }

    public async Task<AuthResult> RegisterAsync(RegisterModel model)
    {
        await Task.Delay(DelayMs);
        return new AuthResult(true);
    }

    public async Task<AuthResult> SendPasswordResetAsync(ForgotPasswordModel model)
    {
        await Task.Delay(DelayMs);
        return new AuthResult(true);
    }
}
