using System.ComponentModel.DataAnnotations;

namespace BIRa.Models;

public class ForgotPasswordModel
{
    [Required(ErrorMessage = "Enter the email you signed up with.")]
    [EmailAddress(ErrorMessage = "That doesn’t look like an email address.")]
    public string Email { get; set; } = "";
}
