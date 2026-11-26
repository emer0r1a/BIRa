namespace BIRa.Models;

/// The choices offered when adding or editing a business.
public static class BusinessOptions
{
    public const string Flat8 = "8% flat income tax";
    public const string GraduatedOsd = "Graduated + OSD";
    public const string GraduatedItemized = "Graduated + itemized";

    public const string IncomeTaxOnly = "Income tax only";
    public const string PercentageTax = "Percentage Tax";
    public const string Vat = "VAT";

    public static readonly string[] Kinds =
    {
        "Freelancer",
        "Self-employed professional",
        "Sole proprietor",
        "Micro business owner",
    };

    public static readonly string[] Regimes = { Flat8, GraduatedOsd, GraduatedItemized };

    public static readonly string[] TaxTypes = { IncomeTaxOnly, PercentageTax, Vat };
}
