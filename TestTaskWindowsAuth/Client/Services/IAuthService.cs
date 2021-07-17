using System.Threading.Tasks;
using TestTaskWindowsAuth.Shared;

namespace TestTaskWindowsAuth.Client.Services
{
    public interface IAuthService
    {
        Task<CurrentUserDto> Login();
        Task<CurrentUserDto> CurrentUserInfo();
    }
}