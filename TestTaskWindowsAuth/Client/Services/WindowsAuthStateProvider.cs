using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TestTaskWindowsAuth.Shared;



namespace TestTaskWindowsAuth.Client.Services
{
    public class WindowsAuthStateProvider : AuthenticationStateProvider
    {
        private IToastService _toastService;
        private readonly IAuthService _api;
        private CurrentUserDto _currentUserDto;
        
        public WindowsAuthStateProvider(IAuthService api, IToastService toastService)
        {
            _api = api;
            _toastService = toastService;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetCurrentUser(); 
                if (userInfo.IsAuthenticated)
                {
                    _toastService.ShowSuccess("Logged in!");
                    var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, userInfo.User),
                        }
                        .Concat(
                            _currentUserDto.Claims.Select(claim => new Claim(claim.ClaimTypeLong, claim.ClaimValue)));
                    identity = new ClaimsIdentity(claims, userInfo.AuthType);
                }
            }
            catch (Exception ex)
            {
                _toastService.ShowInfo("Login via windows authentication to see all app's capability!");
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private async Task<CurrentUserDto> GetCurrentUser() 
        {
            if (_currentUserDto != null && _currentUserDto.IsAuthenticated) return _currentUserDto;
                _currentUserDto = await _api.CurrentUserInfo();
            return _currentUserDto;
        }

        public CurrentUserDto GetUser()
        {
            return _currentUserDto;
        }

        public async Task Login()
        {
            try
            {
                _currentUserDto = await _api.Login();
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Invalid login and password");
            }
        }
    }
}