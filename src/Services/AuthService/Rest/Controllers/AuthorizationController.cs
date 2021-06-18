using Kwetter.Services.AuthService.Application.Common.Interfaces.Services;
using Kwetter.Services.AuthService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthoService _authService;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(IAuthoService authService, ILogger<AuthorizationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AddClaims createAuthRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.SetUserClaims(createAuthRequest.Jwt);
                return new OkObjectResult(response);
            }

            return StatusCode(400);
        }

        [HttpPost("assignRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Moderator")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest assignRoleRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.AssignElevatedPermissions(assignRoleRequest.targetId,assignRoleRequest.roleToAssign);
                return new OkObjectResult(response);
            }

            return StatusCode(400);
        }
    }
}
