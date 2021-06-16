using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Domain;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Kwetter.Services.KweetService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.KweetService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class KweetController : ControllerBase
    {
        private readonly ILogger<KweetController> _logger;
        private readonly IKweetService _kweetService;

        public KweetController(ILogger<KweetController> logger, IKweetService kweetServiceToUse)
        {
            _logger = logger;
            _kweetService = kweetServiceToUse;
        }

        [HttpPost("postKweet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateKweetRequest createKweet)
        {
            if(ModelState.IsValid)
            {
                var response = await _kweetService.CreateKweetAsync(new Guid(createKweet.ProfileId), createKweet.Message);
                return new OkObjectResult(response);
            }
            return StatusCode(500);
        }
        [HttpGet("/getTimeline")]
        public async Task<IActionResult> GetKweetsPaginatedTimeline([FromQuery] GetKweetsRequest getKweets)
        {
            if (ModelState.IsValid)
            {
                return new OkObjectResult(_kweetService.GetKweetsByProfilePaginated(getKweets.PageNumber, getKweets.PageSize, new Guid(getKweets.ProfileId)).Result);
            }
            return StatusCode(500);
        }
        [HttpGet]
        public IEnumerable<Kweet> TestEndpoint()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Kweet
            {
                KweetId = Guid.NewGuid(),
                KweetMessage = "Message Test: " + rng.Next(-20, 55),
                KweetPostDate = DateTime.Now.AddDays(index)
            })
            .ToArray();
        }
    }
}
