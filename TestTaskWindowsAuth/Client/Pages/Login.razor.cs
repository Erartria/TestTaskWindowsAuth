using System.Threading.Tasks;

namespace TestTaskWindowsAuth.Client.Pages
{
    public partial class LogIn
    {
        private async Task Login()
        {
            var user = (await authStateTask).User;
            if (!user.Identity.IsAuthenticated)
            {
                await AuthStateProvider.Login();
                NavigationManager.NavigateTo("profile");
            }
        }
    }
}