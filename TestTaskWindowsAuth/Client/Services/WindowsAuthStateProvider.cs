using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using TestTaskWindowsAuth.Client.Pages;
using TestTaskWindowsAuth.Shared;


namespace TestTaskWindowsAuth.Client.Services
{
    public class WindowsAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _api;
        private CurrentUserDto _currentUserDto;
        
        public WindowsAuthStateProvider(IAuthService api)
        {
            _api = api;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetCurrentUser(); 
                if (userInfo.IsAuthenticated)
                {
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
                Console.WriteLine("Authorization failed: " + ex.ToString());
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private async Task<CurrentUserDto> GetCurrentUser() 
        {
            if (_currentUserDto != null && _currentUserDto.IsAuthenticated) return _currentUserDto;
            _currentUserDto = await _api.CurrentUserInfo();
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
                Console.WriteLine("Authorization failed: " + ex.ToString());
            }
        }
    }
}