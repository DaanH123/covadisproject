using BlazorWebApp.Services;
using BlazorWebApp.State;
using covadis.Shared.Clients;
using covadis.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages.Login
{
    [AllowAnonymous]
    [Route("/login")]
    public partial class Login
    {
        private readonly LoginRequest Request = new();

        [Inject]
        private AuthHttpClient AuthHttpClient { get; set; }

        [Inject]
        private LocalStorageService LocalStorageService { get; set; }

        [Inject]
        private CovadisAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private async Task LoginAsync()
        {
            var response = await AuthHttpClient.LoginAsync(Request);

            if (response is null)
            {
                return;
            }

            await LocalStorageService.SetItemAsync("token", response.Token);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();

            NavigationManager.NavigateTo("/");
        }
    }
}
