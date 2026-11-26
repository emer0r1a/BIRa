using System.ComponentModel.DataAnnotations;

namespace BIRa.Models;

/// Fields on the create-account form.
public class RegisterModel
{
    /// Options for the "I am a…" dropdown.
    public static readonly string[] AccountTypes =
    {
        "Freelancer",
        "Self-employed professional",
        "Micro or small business owner",
        "Sole proprietor",
    };

    [Required(ErrorMessage = "Enter your full name.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Name must be 2 to 80 characters.")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Enter your email address.")]
    [EmailAddress(ErrorMessage = "That doesn’t look like an email address.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Tell us which describes you best.")]
    public string AccountType { get; set; } = "";

    [Required(ErrorMessage = "Create a password.")]
    [MinLength(8, ErrorMessage = "Use at least 8 characters.")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "Include at least one letter and one number.")]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Re-enter your password.")]
    [Compare(nameof(Password), ErrorMessage = "Passwords don’t match.")]
    public string ConfirmPassword { get; set; } = "";

    [Range(typeof(bool), "true", "true", ErrorMessage = "Please confirm to continue.")]
    public bool AcceptTerms { get; set; }
}
