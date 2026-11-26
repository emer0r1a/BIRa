namespace BIRa.Models;

/// Sample businesses and deadlines. Dates are built relative to today so every status
/// (upcoming, due soon, overdue, filed) always shows up, whenever you run the app.
public static class SampleData
{
    public static readonly List<Business> Businesses = new()
    {
        new() { Id = 1, Name = "Maria Reyes, Design", Kind = "Freelancer", Regime = "8% flat income tax", TaxType = "Income tax only", RdoCode = "083", RdoName = "Cebu City North", AvatarClass = "bg-[#ADEE68] text-[#001F3E]" },
        new() { Id = 2, Name = "Kape ni Ate Joy", Kind = "Sole proprietor", Regime = "Graduated + OSD", TaxType = "Percentage Tax", RdoCode = "083", RdoName = "Cebu City North", AvatarClass = "bg-[#1FBCCF] text-[#001F3E]" },
        new() { Id = 3, Name = "JR Print Hub", Kind = "Sole proprietor", Regime = "Graduated + itemized", TaxType = "VAT", RdoCode = "084", RdoName = "Cebu City South", AvatarClass = "bg-[#DCEBFA] text-[#0A4F99]" },
    };

    private static readonly Dictionary<string, string[]> Requirements = new()
    {
        ["1701Q"] = new[] { "Total gross receipts or sales for the quarter", "Form 2307 certificates from your clients", "Your Certificate of Registration (Form 2303)", "Payments you already made this year" },
        ["1701A"] = new[] { "Summary of receipts and expenses for the year", "All Form 2307 certificates for the year", "Your quarterly returns and payment proofs", "Books of accounts or a summary sheet" },
        ["2551Q"] = new[] { "Gross receipts for the quarter", "Official receipts or invoices issued", "Your Certificate of Registration (Form 2303)" },
        ["2550Q"] = new[] { "Summary of sales for the quarter", "Invoices for your input VAT", "Last quarter’s return and any carried-over credits" },
    };

    /// <summary>Avatar colours handed out to businesses the user adds. Amber and red stay reserved for alerts.</summary>
    public static readonly string[] AvatarPalette =
    {
        "bg-[#ADEE68] text-[#001F3E]",
        "bg-[#1FBCCF] text-[#001F3E]",
        "bg-[#DCEBFA] text-[#0A4F99]",
        "bg-[#0F69C5] text-white",
    };

    private static Deadline Build(int id, int businessId, string code, string title, string period, int dayOffset, DateOnly today, bool filed = false)
    {
        var (due, note) = PhCalendar.AdjustToWorkingDay(today.AddDays(dayOffset));
        return new Deadline
        {
            Id = id,
            BusinessId = businessId,
            FormCode = code,
            Title = title,
            Period = period,
            DueDate = due,
            AdjustmentNote = note,
            IsFiled = filed,
            FiledOn = filed ? due.AddDays(-2) : null,
            Requirements = Requirements[code].ToList(),
        };
    }

    /// <summary>The percentage tax or VAT return that matches a business's tax type, if it has one.</summary>
    public static Deadline? CreateBusinessTaxDeadline(Business business, DateOnly today, int id) => business.TaxType switch
    {
        BusinessOptions.PercentageTax => Build(id, business.Id, "2551Q", "Percentage tax return", "Next quarter", 49, today),
        BusinessOptions.Vat => Build(id, business.Id, "2550Q", "Quarterly VAT return", "Next quarter", 49, today),
        _ => null,
    };

    /// <summary>A starting set of deadlines for a newly added business.</summary>
    public static List<Deadline> CreateStarterDeadlines(Business business, DateOnly today, int firstId)
    {
        var id = firstId;
        var list = new List<Deadline>
        {
            Build(id++, business.Id, "1701Q", "Quarterly income tax return", "Next quarter", 34, today),
            Build(id++, business.Id, "1701A", "Annual income tax return", "2026", 140, today),
        };

        var tax = CreateBusinessTaxDeadline(business, today, id);
        if (tax is not null) list.Add(tax);

        return list;
    }

    public static List<Deadline> CreateDeadlines(DateOnly today)
    {
        var list = new List<Deadline>();
        var id = 1;

        void Add(int businessId, string code, string title, string period, int dayOffset, bool filed = false) =>
            list.Add(Build(id++, businessId, code, title, period, dayOffset, today, filed));

        // Maria Reyes, Design: freelancer on 8%. Nothing overdue, so the empty state shows.
        Add(1, "1701Q", "Quarterly income tax return", "Q3 2026", 9);
        Add(1, "1701A", "Annual income tax return", "2026", 140);
        Add(1, "1701Q", "Quarterly income tax return", "Q2 2026", -81, filed: true);
        Add(1, "1701Q", "Quarterly income tax return", "Q1 2026", -172, filed: true);

        // Kape ni Ate Joy: percentage tax filer.
        Add(2, "1701Q", "Quarterly income tax return", "Q3 2026", 4);
        Add(2, "2551Q", "Percentage tax return", "Q4 2026", 60);
        Add(2, "1701A", "Annual income tax return", "2026", 140);
        Add(2, "2551Q", "Percentage tax return", "Q3 2026", -32, filed: true);
        Add(2, "1701Q", "Quarterly income tax return", "Q2 2026", -86, filed: true);

        // JR Print Hub: VAT filer with an overdue return.
        Add(3, "2550Q", "Quarterly VAT return", "Q3 2026", -12);
        Add(3, "2550Q", "Quarterly VAT return", "Q4 2026", 60);
        Add(3, "1701A", "Annual income tax return", "2026", 140);
        Add(3, "1701Q", "Quarterly income tax return", "Q3 2026", -11, filed: true);
        Add(3, "2550Q", "Quarterly VAT return", "Q2 2026", -104, filed: true);

        return list;
    }
}
