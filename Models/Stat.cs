namespace BIRa.Models;

/// A big number with a small label, used in the hero and the milestones row.
public class Stat
{
    public string Value { get; set; } = "";

    /// Shown after the value in the accent colour, e.g. "+" or "%".
    public string Suffix { get; set; } = "";

    public string Label { get; set; } = "";
}
