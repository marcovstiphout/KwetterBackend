using Kwetter.Services.ProfileService.Application.Common.Interfaces.Services;
using Kwetter.Services.ProfileService.Rest.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IProfileService _profileService;

        public ProfileController(ILogger<ProfileController> logger, IProfileService profileServiceToUse)
        {
            _logger = logger;
            _profileService = profileServiceToUse;
        }
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateProfileRequest createProfile)
        {
            if (ModelState.IsValid)
            {
                var response = await _profileService.CreateProfileAsync(createProfile.ProfileName,new Guid(createProfile.ProfileId));
                return new OkObjectResult(response);
            }
            return StatusCode(500);
        }
    }
}
