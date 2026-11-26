namespace BIRa.Models;

/// A step-by-step workflow. Its steps come from <see cref="GuideCatalog.StepsFor"/>.
public class Guide
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Icon { get; set; } = "📋";

    /// "Registration", "Filing" or "Records".
    public string Category { get; set; } = "";

    public int Minutes { get; set; }

    /// True when the guide is tied to the business's next filing, so the page shows the linked deadline.
    public bool LinkedToDeadline { get; set; }
}
