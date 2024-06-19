namespace BlazorWebApp.Layout
{
    using BlazorWebApp.State;
    using Microsoft.AspNetCore.Components;

    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private CovadisAuthenticationStateProvider AuthState { get; set; }

        private async Task Logout()
        {
            await AuthState.Logout();
            NavigationManager.NavigateTo("/login");
        }

        private async Task Login()
        {
            NavigationManager.NavigateTo("/login");
        }

        private async Task UsersPage()
        {
            NavigationManager.NavigateTo("/users");
        }

        private async Task ReservationsPage()
        {
            NavigationManager.NavigateTo("/");
        }

        private async Task VehiclesPage()
        {
            NavigationManager.NavigateTo("/vehicles");
        }
    }
}
