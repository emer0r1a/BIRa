namespace BIRa.Models;

/// The guides and their checklists. Requirements differ by LGU and BIR office,
/// so every guide tells people to confirm with their RDO and COR.
public static class GuideCatalog
{
    public const string Registration = "Registration";
    public const string Filing = "Filing";
    public const string Records = "Records";

    public static readonly string[] Categories = { Registration, Filing, Records };

    public static readonly List<Guide> All = new()
    {
        new() { Id = "register", Title = "Register your business", Icon = "🪪", Category = Registration, Minutes = 45,
                Summary = "First-time registration, from business name to BIR registration." },
        new() { Id = "quarterly-filing", Title = "Quarterly filing prep", Icon = "🗓️", Category = Filing, Minutes = 20, LinkedToDeadline = true,
                Summary = "Get your numbers and papers ready before each quarterly return." },
        new() { Id = "form-2307", Title = "Collect Form 2307", Icon = "🧾", Category = Filing, Minutes = 15, LinkedToDeadline = true,
                Summary = "Gather withholding certificates from clients so you can claim them." },
        new() { Id = "summary-sheet", Title = "Summary sheet of sales", Icon = "📊", Category = Records, Minutes = 15, LinkedToDeadline = true,
                Summary = "A simple per-period list of your sales that backs up your return." },
        new() { Id = "books-receipts", Title = "Books and receipts setup", Icon = "📒", Category = Records, Minutes = 30,
                Summary = "Set up the records the BIR expects you to keep." },
    };

    public static Guide? Find(string? id) => All.FirstOrDefault(g => g.Id == id);

    public static List<GuideStep> StepsFor(string guideId, Business business) => guideId switch
    {
        "register" => Register(),
        "quarterly-filing" => QuarterlyFiling(business),
        "form-2307" => Form2307(),
        "summary-sheet" => SummarySheet(),
        "books-receipts" => BooksReceipts(),
        _ => new List<GuideStep>(),
    };

    private static List<GuideStep> Register() => new()
    {
        new() { Id = "type", Title = "Decide how you’ll register",
                Note = "Most freelancers and small sellers register as a sole proprietor under their own name. Confirm which type fits how you earn.",
                Tip = "Mixing a day job with side income? Mention it. It changes which form you use.",
                Documents = new[] { "Valid ID" } },
        new() { Id = "dti", Title = "Register your business name (if you’ll use one)",
                Note = "Trading under a business name instead of your own name? Register it with the DTI first.",
                Documents = new[] { "DTI certificate" } },
        new() { Id = "permits", Title = "Get your barangay clearance and business permit",
                Note = "Your city or municipality issues these. Ask your LGU what it needs, since requirements vary.",
                Documents = new[] { "Barangay clearance", "Business permit" } },
        new() { Id = "bir", Title = "Register with your BIR office (RDO)",
                Note = "Self-employed people and professionals commonly use BIR Form 1901. Your RDO covers the place where you do business.",
                Tip = "Register with the RDO for your business address, not necessarily your home.",
                Documents = new[] { "BIR Form 1901", "Valid ID", "DTI certificate", "Business permit" } },
        new() { Id = "cor", Title = "Receive and display your COR",
                Note = "The Certificate of Registration (Form 2303) lists your tax types and filing obligations. Keep it handy and display it where you do business.",
                Documents = new[] { "Form 2303 (COR)" } },
        new() { Id = "books", Title = "Register your books of accounts",
                Note = "You need books to record your sales and expenses. The “Books and receipts setup” guide covers this.",
                Documents = new[] { "Books of accounts" } },
        new() { Id = "receipts", Title = "Get authority for your receipts or invoices",
                Note = "Customers need receipts or invoices from you. Ask your RDO how to get yours authorized.",
                Documents = new[] { "Official receipts" } },
    };

    private static List<GuideStep> QuarterlyFiling(Business business)
    {
        var steps = new List<GuideStep>
        {
            new() { Id = "receipts", Title = "Gather your receipts and invoices",
                    Note = "Collect every receipt or invoice you issued this quarter. Check for gaps in the numbering.",
                    Tip = "Missing numbers usually mean a voided or lost receipt. Note why, so you can explain it.",
                    Documents = new[] { "Official receipts" } },
            new() { Id = "totals", Title = "Total your gross sales or receipts",
                    Note = "Add up what you earned this quarter before any deductions. Most returns start from this number.",
                    Documents = new[] { "Summary sheet" } },
            new() { Id = "2307", Title = "Collect Form 2307 from your clients",
                    Note = "Clients who withheld tax from your payments should give you this certificate. It can reduce what you owe.",
                    Documents = new[] { "BIR 2307" } },
            new() { Id = "income-return", Title = "Prepare your quarterly income tax return (1701Q)",
                    Note = IncomeReturnNote(business),
                    Documents = new[] { "BIR 1701Q", "Form 2303 (COR)" } },
        };

        var (code, name) = business.TaxType switch
        {
            BusinessOptions.PercentageTax => ("2551Q", "percentage tax"),
            BusinessOptions.Vat => ("2550Q", "VAT"),
            _ => ("", ""),
        };

        if (code != "")
        {
            steps.Add(new GuideStep
            {
                Id = "business-return",
                Title = $"Prepare your {name} return ({code})",
                Note = business.TaxType == BusinessOptions.Vat
                    ? "Your VAT return uses your sales and your input VAT invoices for the quarter."
                    : "Percentage tax is based on your gross receipts for the quarter.",
                Documents = new[] { $"BIR {code}", "Official receipts" },
            });
        }

        steps.Add(new GuideStep
        {
            Id = "file-pay",
            Title = "File and pay on or before the due date",
            Note = "Use the due date shown on your dashboard, then file and pay through BIR’s official channels. Keep your proof.",
            Documents = new[] { "Proof of payment" },
        });

        return steps;
    }

    private static string IncomeReturnNote(Business business) => business.Regime switch
    {
        BusinessOptions.Flat8 => "On the 8% option, the tax is worked out on your gross receipts for the year so far, less the ₱250,000 allowance and payments you already made.",
        BusinessOptions.GraduatedOsd => "On graduated rates with OSD, tax is worked out on your gross less a standard 40% deduction, so you don’t need to list expenses.",
        _ => "On graduated rates with itemized deductions, you subtract your actual, documented business expenses. Keep every receipt.",
    };

    private static List<GuideStep> Form2307() => new()
    {
        new() { Id = "who", Title = "Work out which clients should withhold",
                Note = "Some clients, usually companies, withhold a small percentage of what they pay you and send it to the BIR for you.",
                Documents = Array.Empty<string>() },
        new() { Id = "ask", Title = "Ask each client for their 2307 early",
                Note = "Certificates are usually issued after the quarter ends. Remind clients a week or two before you need them.",
                Tip = "Some clients send them late. Start asking before your filing week.",
                Documents = new[] { "BIR 2307" } },
        new() { Id = "check", Title = "Check every certificate",
                Note = "Make sure your name, TIN, the period and the amount withheld are right. Wrong details can get a certificate rejected.",
                Documents = new[] { "BIR 2307" } },
        new() { Id = "file", Title = "Sort them by quarter and keep copies",
                Note = "You’ll list the withheld amounts in your return and may need to attach the certificates. Keep a copy of everything.",
                Documents = new[] { "BIR 2307", "Summary sheet" } },
    };

    private static List<GuideStep> SummarySheet() => new()
    {
        new() { Id = "setup", Title = "Set up a simple sheet",
                Note = "A spreadsheet is enough. Make columns for date, client, invoice or receipt number, and amount.",
                Documents = new[] { "Summary sheet" } },
        new() { Id = "list", Title = "List every sale for the period",
                Note = "Add each sale in date order. Include sales paid through e-wallets and bank transfer.",
                Documents = new[] { "Official receipts" } },
        new() { Id = "match", Title = "Match the total to your bank and e-wallet",
                Note = "The sheet total should line up with the money you actually received. Investigate any difference.",
                Documents = new[] { "Proof of payment" } },
        new() { Id = "save", Title = "Save it with your return",
                Note = "Keep the sheet together with your filed return and proof of payment so everything is in one place.",
                Documents = new[] { "Summary sheet", "Proof of payment" } },
    };

    private static List<GuideStep> BooksReceipts() => new()
    {
        new() { Id = "choose", Title = "Choose manual or computerized books",
                Note = "Manual books are bound or loose-leaf notebooks. Computerized books need BIR permission. Pick what you’ll actually keep up with.",
                Documents = new[] { "Books of accounts" } },
        new() { Id = "register", Title = "Register your books with the BIR",
                Note = "Books should be registered with your RDO before you use them. Ask what your RDO needs from you.",
                Documents = new[] { "Books of accounts", "Form 2303 (COR)" } },
        new() { Id = "issue", Title = "Issue a receipt or invoice for every sale",
                Note = "Make it a habit. Customers should get one each time, and you keep the duplicate.",
                Documents = new[] { "Official receipts" } },
        new() { Id = "log", Title = "Record sales and expenses regularly",
                Note = "A few minutes each week is easier than a whole quarter at once.",
                Tip = "Photograph paper receipts. They fade, and phones don’t lose them.",
                Documents = new[] { "Summary sheet" } },
        new() { Id = "keep", Title = "Store your records safely",
                Note = "Keep books and supporting documents for as long as the BIR requires. This is commonly ten years, so confirm with your RDO.",
                Documents = new[] { "Books of accounts" } },
    };
}
