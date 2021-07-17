using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TestTaskWindowsAuth.Client.Services;

namespace TestTaskWindowsAuth.Client.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private  WindowsAuthStateProvider WindowsAuthStateProvider { get; set; }
        [CascadingParameter]
        private AuthenticationException AuthenticationException { get; set; }
        private void ToProfilePage()
        {
            NavigationManager.NavigateTo("profile");
        }

        private async Task Authorize()
        {
            await WindowsAuthStateProvider.Login();
        }
        
        
    }
}