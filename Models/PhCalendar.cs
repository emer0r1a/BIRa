namespace BIRa.Models;

/// Weekend and holiday handling for due dates. Sample list of fixed-date Philippine holidays only:
/// movable holidays and BIR advisories are not included, so always confirm with the BIR.
public static class PhCalendar
{
    private static readonly Dictionary<(int Month, int Day), string> Holidays = new()
    {
        [(1, 1)] = "New Year’s Day",
        [(4, 9)] = "Araw ng Kagitingan",
        [(5, 1)] = "Labor Day",
        [(6, 12)] = "Independence Day",
        [(8, 21)] = "Ninoy Aquino Day",
        [(11, 1)] = "All Saints’ Day",
        [(11, 30)] = "Bonifacio Day",
        [(12, 8)] = "Feast of the Immaculate Conception",
        [(12, 25)] = "Christmas Day",
        [(12, 30)] = "Rizal Day",
        [(12, 31)] = "Last Day of the Year",
    };

    public static string? HolidayName(DateOnly date) =>
        Holidays.TryGetValue((date.Month, date.Day), out var name) ? name : null;

    public static bool IsWeekend(DateOnly date) =>
        date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// Moves a due date forward to the next working day and explains why, if it moved.
    public static (DateOnly Date, string? Note) AdjustToWorkingDay(DateOnly raw)
    {
        var date = raw;
        string? reason = null;

        while (IsWeekend(date) || HolidayName(date) is not null)
        {
            reason ??= IsWeekend(date) ? "weekend" : HolidayName(date);
            date = date.AddDays(1);
        }

        return date == raw ? (raw, null) : (date, $"Moved from {raw:ddd, MMM d} ({reason})");
    }
}
