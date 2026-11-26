using BIRa.Models;

namespace BIRa.Services;

/// The signed-in user's businesses, which one is selected, and their deadlines.
/// Components subscribe to <see cref="Changed"/> to re-render when any of it changes.
public class WorkspaceState
{
    private readonly List<Business> businesses;
    private readonly List<Deadline> deadlines;
    private readonly HashSet<string> doneSteps = new();

    public WorkspaceState()
    {
        // Copies, so edits never touch the shared sample data.
        businesses = SampleData.Businesses.Select(b => b.Clone()).ToList();
        Current = businesses[0];
        deadlines = SampleData.CreateDeadlines(Today);
    }

    public string UserName { get; } = "Maria Reyes";

    public DateOnly Today => DateOnly.FromDateTime(DateTime.Now);

    public IReadOnlyList<Business> Businesses => businesses;

    public Business Current { get; private set; }

    /// Deadlines for the selected business, soonest first.
    public IEnumerable<Deadline> CurrentDeadlines => DeadlinesFor(Current.Id);

    public IEnumerable<Deadline> DeadlinesFor(int businessId) =>
        deadlines.Where(d => d.BusinessId == businessId).OrderBy(d => d.DueDate);

    public event Action? Changed;

    public void Select(int businessId)
    {
        var business = businesses.FirstOrDefault(b => b.Id == businessId);
        if (business is null || business == Current) return;

        Current = business;
        Changed?.Invoke();
    }

    public void SetFiled(int deadlineId, bool filed)
    {
        var deadline = deadlines.FirstOrDefault(d => d.Id == deadlineId);
        if (deadline is null) return;

        deadline.IsFiled = filed;
        deadline.FiledOn = filed ? Today : null;
        Changed?.Invoke();
    }

    // ---- Guide checklists. Progress is kept per business, since each business has its own steps.

    private static string StepKey(int businessId, string guideId, string stepId) => $"{businessId}:{guideId}:{stepId}";

    public bool IsStepDone(int businessId, string guideId, string stepId) =>
        doneSteps.Contains(StepKey(businessId, guideId, stepId));

    public void ToggleStep(int businessId, string guideId, string stepId)
    {
        var key = StepKey(businessId, guideId, stepId);
        if (!doneSteps.Remove(key)) doneSteps.Add(key);
        Changed?.Invoke();
    }

    public void ResetGuide(int businessId, string guideId)
    {
        doneSteps.RemoveWhere(k => k.StartsWith($"{businessId}:{guideId}:"));
        Changed?.Invoke();
    }

    /// Adds a business and gives it a starter set of deadlines.
    public Business AddBusiness(BusinessFormModel form)
    {
        var business = new Business
        {
            Id = businesses.Count == 0 ? 1 : businesses.Max(b => b.Id) + 1,
            AvatarClass = SampleData.AvatarPalette[businesses.Count % SampleData.AvatarPalette.Length],
        };
        Apply(business, form);
        businesses.Add(business);

        var firstId = deadlines.Count == 0 ? 1 : deadlines.Max(d => d.Id) + 1;
        deadlines.AddRange(SampleData.CreateStarterDeadlines(business, Today, firstId));

        Changed?.Invoke();
        return business;
    }

    /// Saves edits. If the tax type changed, the unfiled percentage tax / VAT return is swapped to match.
    public void UpdateBusiness(int businessId, BusinessFormModel form)
    {
        var business = businesses.FirstOrDefault(b => b.Id == businessId);
        if (business is null) return;

        var taxTypeChanged = business.TaxType != form.TaxType;
        Apply(business, form);

        if (taxTypeChanged)
        {
            deadlines.RemoveAll(d => d.BusinessId == businessId && !d.IsFiled && d.FormCode is "2551Q" or "2550Q");

            var nextId = deadlines.Count == 0 ? 1 : deadlines.Max(d => d.Id) + 1;
            var replacement = SampleData.CreateBusinessTaxDeadline(business, Today, nextId);
            if (replacement is not null) deadlines.Add(replacement);
        }

        Changed?.Invoke();
    }

    private static void Apply(Business business, BusinessFormModel form)
    {
        business.Name = form.Name.Trim();
        business.Kind = form.Kind;
        business.Regime = form.Regime;
        business.TaxType = form.TaxType;
        business.RdoCode = form.RdoCode.Trim();
        business.RdoName = form.RdoName.Trim();
    }
}
