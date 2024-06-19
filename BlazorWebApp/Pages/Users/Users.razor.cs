using BlazorWebApp.State;
using covadis.Shared.Clients;
using covadis.Shared.Constants;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

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

        protected override async Task OnInitializedAsync()
        {
            users = await UserHttpClient.GetUsersAsync();
        }

        private void AddNewUser()
        {
            NavigationManager.NavigateTo("/user/create");
        }
    }
}
