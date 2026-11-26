using System.ComponentModel.DataAnnotations;

namespace BIRa.Models;

/// Fields on the log-in form.
public class LoginModel
{
    [Required(ErrorMessage = "Enter your email address.")]
    [EmailAddress(ErrorMessage = "That doesn’t look like an email address.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Enter your password.")]
    public string Password { get; set; } = "";

    public bool RememberMe { get; set; }
}
