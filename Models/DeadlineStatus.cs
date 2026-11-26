namespace BIRa.Models;

public enum DeadlineStatus
{
    Upcoming,
    DueSoon,
    Overdue,
    Filed,
}

/// Labels and Tailwind classes for each status, so every component colours them the same way.
/// Amber and red are reserved for deadline and penalty states.
public static class DeadlineStatusExtensions
{
    public static string Label(this DeadlineStatus s) => s switch
    {
        DeadlineStatus.Upcoming => "Upcoming",
        DeadlineStatus.DueSoon => "Due soon",
        DeadlineStatus.Overdue => "Overdue",
        _ => "Filed",
    };

    public static string Hint(this DeadlineStatus s) => s switch
    {
        DeadlineStatus.Upcoming => "More than 2 weeks away",
        DeadlineStatus.DueSoon => "Within the next 2 weeks",
        DeadlineStatus.Overdue => "Needs your attention",
        _ => "Done and dusted",
    };

    /// Pill background + text colour (lime and amber always carry navy text).
    public static string BadgeClass(this DeadlineStatus s) => s switch
    {
        DeadlineStatus.Upcoming => "bg-[#DCEBFA] text-[#0A4F99]",
        DeadlineStatus.DueSoon => "bg-[#FEB21A] text-[#001F3E]",
        DeadlineStatus.Overdue => "bg-[#ED3F27] text-white",
        _ => "bg-[#ADEE68] text-[#001F3E]",
    };

    /// Solid colour for dots and bars.
    public static string DotClass(this DeadlineStatus s) => s switch
    {
        DeadlineStatus.Upcoming => "bg-[#0F69C5]",
        DeadlineStatus.DueSoon => "bg-[#FEB21A]",
        DeadlineStatus.Overdue => "bg-[#ED3F27]",
        _ => "bg-[#ADEE68]",
    };
}
