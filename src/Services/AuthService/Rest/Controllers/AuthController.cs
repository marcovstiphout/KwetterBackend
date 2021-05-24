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

        [HttpPost("")]
        public async Task<IActionResult> Register([FromBody] AuthorizationRequest authorizationRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _authService.AuthorizeAsync(authorizationRequest.Code);
            return new OkObjectResult(response);
        }
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        //ToDo: Make this more SOLID by splitting responsibilities. Possibly even extracting the Google Methods into a seperate file.
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            //Check if Account Exists
            string email = claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            if(_authService.CheckAccountExistsAsync(email).Result)
            {
                //If exists, create a new JWT and return it
                var test = "test";
            }
            else
            {
                //If it does not exist, register account and place message on Queue to create a new Profile.
                await _authService.CreateAccountAsync(email, "Test");
                _publisher.PublishMessageAsync<string>("AccountCreated", "MarcoTest");
            }
            
            return new JsonResult(claims);
        }
    }
}
