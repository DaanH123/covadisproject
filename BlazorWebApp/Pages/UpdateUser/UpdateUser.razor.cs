using covadis.Shared.Constants;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages.UpdateUser
{
    [Route("/user/edit/{userId}")]
    [Authorize(Roles = Roles.Administrator)]
    public partial class UpdateUser
    {
        [Parameter]
        public string userId { get; set; }

        private UserResponse User { get; set; }
        private UpdateUserRequest Request { get; set; } = new UpdateUserRequest();

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private UserHttpClient UserHttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(userId, out int id);
            User = await UserHttpClient.GetUserAsync(id);
            if (User != null)
            {
                Request.Name = User.Name;
                Request.Email = User.Email;
            }
        }

        private async Task UpdateUserAsync()
        {
            int.TryParse(userId, out int id);
            var updatedUser = await UserHttpClient.UpdateUserAsync(id, Request);
            if (updatedUser != null)
            {
                NavigationManager.NavigateTo("/users");
            }
        }
    }
}