using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using DevExpress.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTaskWindowsAuth.Server.Extensions;
using TestTaskWindowsAuth.Shared;

namespace TestTaskWindowsAuth.Server.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class WindowsAuth : ControllerBase
    {
        private readonly ILogger<WindowsAuth> _logger;

        public WindowsAuth(ILogger<WindowsAuth> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet(nameof(Login))]
        public ActionResult<CurrentUserDto> Login()
        { 
            if (HttpContext.User.Identity is WindowsIdentity windowsIdentity)
            {
                var devices = from device in windowsIdentity.DeviceClaims
                    select new ClaimDto
                    {
                        ClaimTypeLong = device.Type,
                        ClaimTypeShort = device.Type.SplitAndGetLastElement(),
                        ClaimValue = device.Value
                    };

                var claims = from claim in windowsIdentity.Claims
                    select new ClaimDto
                    {
                        ClaimTypeLong = claim.Type,
                        ClaimTypeShort = claim.Type.SplitAndGetLastElement(),
                        ClaimValue = claim.Value
                    };

                var groups = from id in windowsIdentity.Groups
                    select new GroupDto
                    {
                        GroupId = id.Value,
                        GroupName = id.Translate(typeof(NTAccount)).Value.SplitAndGetLastElement(),
                        Domain = id.Translate(typeof(NTAccount)).Value.SplitAndGetFirstElement() !=
                                 id.Translate(typeof(NTAccount)).Value.SplitAndGetLastElement()
                            ? id.Translate(typeof(NTAccount)).Value.SplitAndGetFirstElement()
                            : "-"
                    };


                var result = new CurrentUserDto
                {
                    IsAuthenticated = windowsIdentity.IsAuthenticated,
                    IsSystem = windowsIdentity.IsSystem,
                    IsGuest = windowsIdentity.IsGuest,
                    IsAnonymous = windowsIdentity.IsAnonymous,
                    AccessTokenIsClosed = windowsIdentity.AccessToken.IsClosed,
                    Token = windowsIdentity.Token.ToString(),
                    User = windowsIdentity.Name.SplitAndGetLastElement(),
                    Domain = windowsIdentity.Name.SplitAndGetFirstElement(),
                    Claims = claims,
                    Groups = groups,
                    AuthType = windowsIdentity.AuthenticationType,
                    Devices = devices
                };
                return Ok(result);
            }

            _logger.LogError("User isn't authorized!");
            return BadRequest();
        }
    }
}