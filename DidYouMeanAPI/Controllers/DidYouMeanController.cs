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
        public async Task<DidYouMean> GetAsync()
        {
            Console.WriteLine($"Starting at {DateTime.Now.TimeOfDay}");
            DidYouMean words = await MySlowTask();
            Console.WriteLine($"Done at {DateTime.Now.TimeOfDay}");
            return words;
        }

        private async Task<DidYouMean> MySlowTask()
        {
            await Task.Delay(5000);
            return new DidYouMean { Words = Summaries.ToList() };
        }
    }
}