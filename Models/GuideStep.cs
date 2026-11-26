namespace BIRa.Models;

/// One checklist item inside a guide.
public class GuideStep
{
    /// Stable id within its guide (progress is saved against it).
    public string Id { get; set; } = "";

    public string Title { get; set; } = "";

    /// Plain-language explanation shown under the title.
    public string Note { get; set; } = "";

    /// Optional "common mistake" shown in a collapsible.
    public string? Tip { get; set; }

    /// Document tags, e.g. "BIR 2307" or "Official receipts".
    public string[] Documents { get; set; } = Array.Empty<string>();
}
