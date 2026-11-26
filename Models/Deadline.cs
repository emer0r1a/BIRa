namespace BIRa.Models;

/// One filing or payment due date for a business.
public class Deadline
{
    /// How many days ahead a filing counts as "Due soon".
    public const int DueSoonDays = 14;

    public int Id { get; set; }
    public int BusinessId { get; set; }

    /// BIR form number, e.g. "2551Q".
    public string FormCode { get; set; } = "";

    /// Plain-language name, e.g. "Percentage tax return".
    public string Title { get; set; } = "";

    public string Period { get; set; } = "";

    /// The working-day date the filing is actually due.
    public DateOnly DueDate { get; set; }

    /// Set when the date was moved off a weekend or holiday, e.g. "Moved from Sun, Oct 25 (weekend)".
    public string? AdjustmentNote { get; set; }

    public bool IsFiled { get; set; }
    public DateOnly? FiledOn { get; set; }

    public List<string> Requirements { get; set; } = new();

    public DeadlineStatus StatusOn(DateOnly today)
    {
        if (IsFiled) return DeadlineStatus.Filed;
        if (DueDate < today) return DeadlineStatus.Overdue;
        return DueDate.DayNumber - today.DayNumber <= DueSoonDays ? DeadlineStatus.DueSoon : DeadlineStatus.Upcoming;
    }

    public string RelativeText(DateOnly today)
    {
        if (IsFiled) return FiledOn is { } f ? $"Filed {f:MMM d}" : "Filed";

        var days = DueDate.DayNumber - today.DayNumber;
        return days switch
        {
            0 => "Due today",
            1 => "Due tomorrow",
            > 1 => $"Due in {days} days",
            -1 => "1 day overdue",
            _ => $"{-days} days overdue",
        };
    }
}
