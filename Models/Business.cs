namespace BIRa.Models;

/// A registration (business or freelance profile) the user manages, as listed on its COR / Form 2303.
public class Business
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    /// e.g. "Freelancer" or "Sole proprietor".
    public string Kind { get; set; } = "";

    /// e.g. "8% flat income tax" or "Graduated + OSD".
    public string Regime { get; set; } = "";

    /// e.g. "Percentage Tax" or "VAT".
    public string TaxType { get; set; } = "";

    /// The 3-digit Revenue District Office code on the COR, e.g. "083".
    public string RdoCode { get; set; } = "";

    /// Optional office or area name, e.g. "Cebu City North".
    public string RdoName { get; set; } = "";

    /// How the RDO is shown in the UI, e.g. "RDO 083 · Cebu City North".
    public string Rdo => string.IsNullOrWhiteSpace(RdoName) ? $"RDO {RdoCode}" : $"RDO {RdoCode} · {RdoName}";

    /// Tailwind size and colour utilities for this business's avatar.
    public string AvatarClass { get; set; } = "bg-[#ADEE68] text-[#001F3E]";

    public Business Clone() => (Business)MemberwiseClone();
}
