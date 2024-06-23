using BlazorWebApp.State;
using covadis.Shared.Clients;
using covadis.Shared.Constants;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWebApp.Pages.Users
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("/users")]
    public partial class Users : ComponentBase
    {
        private IEnumerable<UserResponse> users { get; set; }

        [Inject]
        private CovadisAuthenticationStateProvider AuthState { get; set; }

        [Inject]
        private UserHttpClient UserHttpClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            users = await UserHttpClient.GetUsersAsync();
        }

        private void AddNewUser()
        {
            NavigationManager.NavigateTo("/user/create");
        }

        private void EditUser(int userId)
        {
            NavigationManager.NavigateTo($"/user/edit/{userId}");
        }

        private async Task DeleteUser(int userId)
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", new object[] { "Are you sure you want to delete this user?" });
            if (confirmed)
            {
                var result = await UserHttpClient.DeleteUserAsync(userId);
                if (result)
                {
                    users = await UserHttpClient.GetUsersAsync();
                }
            }
        }
    }
}
