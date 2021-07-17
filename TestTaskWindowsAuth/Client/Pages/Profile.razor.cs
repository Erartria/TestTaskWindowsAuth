using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestTaskWindowsAuth.Shared;

namespace TestTaskWindowsAuth.Client.Pages
{
    public partial class Profile
    {
        private CurrentUserDto _currentUserDto;

        protected override async Task OnInitializedAsync()
        {
            _currentUserDto = await WindowsAuthService.CurrentUserInfo();
        }

        async Task<IEnumerable<CurrentUserDto>> LoadDataAsync(CancellationToken token)
        {
            return new List<CurrentUserDto>() {_currentUserDto};
        }
    }
}