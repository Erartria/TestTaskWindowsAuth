using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;
using TestTaskWindowsAuth.Shared;

namespace TestTaskWindowsAuth.Client.Services
{
    public class WindowsAuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public WindowsAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CurrentUserDto> CurrentUserInfo()
        {
            var result = await _httpClient.GetFromJsonAsync<CurrentUserDto>("api/windowsauth/login");
            return result;
        }
        
        public async Task<CurrentUserDto> Login()
        {
            var result = await _httpClient.GetAsync("api/windowsauth/login");
            if ((int)result.StatusCode != 200)
            {
                throw new UnauthorizedAccessException("Can't login by windows authentication!");
            }
                
            return await result.Content.ReadFromJsonAsync<CurrentUserDto>();

        }
    }
}