using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Domain;

namespace Kwetter.KweetService.Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KweetController : ControllerBase
    {
        private readonly ILogger<KweetController> _logger;

        public KweetController(ILogger<KweetController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Kweet> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Kweet
            {
                Id = Guid.NewGuid(),
                KweetMessage = "Message Test: " + rng.Next(-20, 55),
                KweetPostDate = DateTime.Now.AddDays(index)
            })
            .ToArray();
        }
    }
}
