using Microsoft.AspNetCore.Mvc;

namespace DidYouMeanAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DidYouMeanController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<DidYouMeanController> _logger;

        public DidYouMeanController(ILogger<DidYouMeanController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "DidYouMean")]
        public IEnumerable<DidYouMean> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new DidYouMean
            {
                Words = Summaries.ToList()
            })
            .ToArray();
        }
    }
}