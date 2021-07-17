using System.Threading.Tasks;

namespace TestTaskWindowsAuth.Client.Shared
{
    public partial class MainLayout
    {
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