namespace BIRa.Models;

/// One card in the "How it works" section.
public class Step
{
    public int Number { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

    /// Tailwind background utility for the number badge, e.g. "bg-[#ADEE68]".
    public string ColorClass { get; set; } = "bg-[#ADEE68]";
}
