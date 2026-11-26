using System.ComponentModel.DataAnnotations;

namespace BIRa.Models;

/// Fields on the add / edit business form. Mirrors what's printed on a COR (Form 2303).
public class BusinessFormModel : IValidatableObject
{
    [Required(ErrorMessage = "Give this business a name.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "Name must be 2 to 60 characters.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Choose a registration type.")]
    public string Kind { get; set; } = "";

    [Required(ErrorMessage = "Choose an income tax regime.")]
    public string Regime { get; set; } = "";

    [Required(ErrorMessage = "Choose a tax type.")]
    public string TaxType { get; set; } = "";

    [Required(ErrorMessage = "Enter your RDO code.")]
    [RegularExpression(@"^\d{3}$", ErrorMessage = "RDO codes are 3 digits, like 083.")]
    public string RdoCode { get; set; } = "";

    [StringLength(40, ErrorMessage = "Keep this under 40 characters.")]
    public string RdoName { get; set; } = "";

    public static BusinessFormModel From(Business b) => new()
    {
        Name = b.Name,
        Kind = b.Kind,
        Regime = b.Regime,
        TaxType = b.TaxType,
        RdoCode = b.RdoCode,
        RdoName = b.RdoName,
    };

    /// Checks that the regime and tax type go together.
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Regime == BusinessOptions.Flat8 && TaxType != BusinessOptions.IncomeTaxOnly)
        {
            yield return new ValidationResult(
                "The 8% option replaces percentage tax, and can’t be used with VAT. Choose “Income tax only”.",
                new[] { nameof(TaxType) });
        }
        else if (Regime != BusinessOptions.Flat8 && TaxType == BusinessOptions.IncomeTaxOnly)
        {
            yield return new ValidationResult(
                "Graduated rates come with a business tax. Choose Percentage Tax or VAT.",
                new[] { nameof(TaxType) });
        }
    }
}
