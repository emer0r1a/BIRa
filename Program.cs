using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BIRa.Components;
using BIRa.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Demo auth. Replace with a real IAuthService (e.g. Supabase) when the backend exists.
builder.Services.AddScoped<IAuthService, MockAuthService>();

// Businesses and deadlines shared by the dashboard layout and pages (sample data for now).
builder.Services.AddScoped<WorkspaceState>();

await builder.Build().RunAsync();
