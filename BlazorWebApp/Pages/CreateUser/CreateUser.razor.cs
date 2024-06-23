using covadis.Shared.Clients;
using covadis.Shared.Constants;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebApp.Pages.CreateUser
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("user/create")]
    public partial class CreateUser : ComponentBase
    {
        private bool isLoading = true;
        private CreateUserRequest Request = new CreateUserRequest();
        private IEnumerable<RoleResponse> AvailableRoles;
        private IEnumerable<string> Errors = new List<string>();
        private int SelectedRoleId;

        [Inject]
        private UserHttpClient UserHttpClient { get; set; }

        [Inject]
        private RoleHttpClient RoleHttpClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadRoles();
        }

        private async Task LoadRoles()
        {
            AvailableRoles = await RoleHttpClient.GetRolesAsync();
            isLoading = false;
        }

        private async Task CreateUserAsync()
        {
            // Assign the selected role ID to the request
            Request.Roles = new List<string> { AvailableRoles.FirstOrDefault(r => r.Id == SelectedRoleId)?.Name };

            try
            {
                var response = await UserHttpClient.CreateUserAsync(Request);
                NavigationManager.NavigateTo("/users");
            }
            catch (Exception ex)
            {
                Errors = new List<string> { ex.Message };
            }
        }
    }
}
