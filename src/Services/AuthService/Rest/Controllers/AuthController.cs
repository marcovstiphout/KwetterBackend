using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Rest.Models.Requests;
using Kwetter.Services.Shared.Messaging.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private IMessagePublisher _publisher;

        public AuthController(IAuthService authService, IMessagePublisher publisher)
        {
            _authService = authService;
            _publisher = publisher;
        }

        //[HttpPost("")]
        [Route("authorize")]
        public async Task<IActionResult> Register()
        {
            if (!ModelState.IsValid) return BadRequest();
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        //ToDo: Make this more SOLID by splitting responsibilities. Possibly even extracting the Google Methods into a seperate file.
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _authService.AuthorizeAsync(result);

            return Ok();
        }
    }
}
