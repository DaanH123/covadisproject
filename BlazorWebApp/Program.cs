using BlazorWebApp.Handlers;
using BlazorWebApp.Services;
using BlazorWebApp;
using covadis.Shared.Clients;
using covadis.Shared.Options;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebApp.State;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // Add options pattern to the configuration
        builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection(ApiOptions.SectionName));

        // Add Services
        builder.Services.AddScoped<LocalStorageService>();

        // Add API Clients and Handlers
        builder.Services.AddScoped<AuthorizationMessageHandler>();

        builder.Services.AddScoped<UserHttpClient>();
        builder.Services.AddHttpClient(nameof(UserHttpClient)).AddHttpMessageHandler<AuthorizationMessageHandler>();

        builder.Services.AddScoped<ReservationHttpClient>();
        builder.Services.AddHttpClient(nameof(ReservationHttpClient)).AddHttpMessageHandler<AuthorizationMessageHandler>();

        builder.Services.AddScoped<VehicleHttpClient>();
        builder.Services.AddHttpClient(nameof(VehicleHttpClient)).AddHttpMessageHandler<AuthorizationMessageHandler>();

        builder.Services.AddScoped<AuthHttpClient>();

        // Add Auth
        builder.Services.AddScoped<CovadisAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CovadisAuthenticationStateProvider>());
        builder.Services.AddAuthorizationCore();


        await builder.Build().RunAsync();
    }
}