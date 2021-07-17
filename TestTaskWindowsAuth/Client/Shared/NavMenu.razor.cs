using System.Threading.Tasks;

namespace TestTaskWindowsAuth.Client.Shared
{
    partial class NavMenu
    {
        private bool collapseNavMenu = true;

        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
        private async Task Login()
        {
            var user = (await authStateTask).User;
            if (!user.Identity.IsAuthenticated)
            {
                await AuthStateProvider.Login();
                NavigationManager.NavigateTo("/");
            }
        }
    }
}