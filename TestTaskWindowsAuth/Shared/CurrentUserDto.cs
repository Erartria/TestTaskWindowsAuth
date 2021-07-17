
using System.Collections.Generic;

namespace TestTaskWindowsAuth.Shared
{
    public class CurrentUserDto
    {
        public bool IsAuthenticated { get; set; }
        public bool IsSystem { get; set; }
        public bool IsGuest { get; set; }
        public bool IsAnonymous { get; set; }
        public bool AccessTokenIsClosed { get; set; }
        public string Token { get; set; }
        public string User { get; set; }
        public string Domain { get; set; }
        public string AuthType { get; set; }
        public IEnumerable<ClaimDto> Claims { get; set; }
        public IEnumerable<GroupDto> Groups { get; set; }
        public IEnumerable<ClaimDto> Devices { get; set; }
    }
}