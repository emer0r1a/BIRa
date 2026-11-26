namespace BIRa.Models;

/// Sample marketing content for the landing page. Edit the numbers and copy here, not in Home.razor.
public static class LandingData
{
    public static readonly List<Stat> HeroStats = new()
    {
        new() { Value = "12K", Suffix = "+", Label = "Freelancers guided" },
        new() { Value = "48",  Suffix = "+", Label = "BIR forms in plain English" },
    };

    public static readonly List<Stat> Milestones = new()
    {
        new() { Value = "98",  Suffix = "%", Label = "Filed on time" },
        new() { Value = "10K", Suffix = "+", Label = "Businesses assisted" },
        new() { Value = "1M",  Suffix = "+", Label = "Reminders sent" },
        new() { Value = "17",  Suffix = "",  Label = "Regions covered" },
        new() { Value = "24/7", Suffix = "", Label = "Guides on hand" },
    };

    /// Form codes for the scrolling strip under the hero.
    public static readonly List<(string Code, string Label)> Forms = new()
    {
        ("1701Q", "quarterly income tax"),
        ("2551Q", "percentage tax"),
        ("2550Q", "quarterly VAT"),
        ("2307",  "withholding certificate"),
        ("2303",  "your COR"),
        ("1701A", "annual income tax"),
        ("2316",  "compensation certificate"),
    };

    public static readonly List<Step> Steps = new()
    {
        new() { Number = 1, Title = "Add your business", Description = "Enter the details on your Certificate of Registration (Form 2303): tax type, regime and RDO.", ColorClass = "bg-[#ADEE68]" },
        new() { Number = 2, Title = "See your calendar", Description = "Get your filing dates, already moved for weekends, holidays and advisories.", ColorClass = "bg-[#1FBCCF]" },
        new() { Number = 3, Title = "Prep and file", Description = "Follow a short checklist, preview what you owe, then file through BIR’s official channels.", ColorClass = "bg-[#FEB21A]" },
    };

    public static readonly List<(string Icon, string Title, string Blurb)> Audiences = new()
    {
        ("💻", "Freelancers", "Designers, developers, VAs, writers and other online professionals."),
        ("🧑‍🔧", "Self-employed", "Tutors, photographers, drivers and other professionals working for themselves."),
        ("🏪", "Micro businesses", "Small shops, food stalls and home-based sellers just getting started."),
        ("🧾", "Sole proprietors", "Owners juggling more than one registration or tax type."),
    };

    public static readonly List<Faq> Faqs = new()
    {
        new() { Question = "Is BIRa an official BIR service?", Answer = "No. BIRa is an independent, informational aid. Always verify deadlines and tax types against your official BIR Form 2303 COR and current BIR issuances." },
        new() { Question = "Do I need to know tax terms to use it?", Answer = "No. Every guide, label and calculator result is written in plain language, with the official form name shown next to it so you can match it to what BIR asks for." },
        new() { Question = "Can I manage more than one business?", Answer = "Yes. Each business or freelance profile keeps its own tax regime, RDO and deadlines, and you can switch between them from the top of the app." },
        new() { Question = "Does BIRa file my taxes for me?", Answer = "No. BIRa helps you understand what to file, prepare your documents and estimate what you may owe. You still file and pay through BIR’s official channels." },
        new() { Question = "How are my due dates worked out?", Answer = "From the forms and tax types on your COR, adjusted for weekends, holidays and BIR advisories. Treat them as a reminder and confirm with the BIR when in doubt." },
    };
}
