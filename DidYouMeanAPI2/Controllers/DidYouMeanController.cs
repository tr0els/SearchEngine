using Microsoft.AspNetCore.Mvc;

namespace DidYouMeanAPI2.Controllers
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
            var name = "#2";
            Console.WriteLine($"{DateTime.Now.TimeOfDay} - DidYouMeanAPI {name} starting slow task");
            DidYouMean words = await MySlowTask();
            Console.WriteLine($"{DateTime.Now.TimeOfDay} - DidYouMeanAPI {name} done with slow task");
            return words;
        }

        private async Task<DidYouMean> MySlowTask()
        {
            await Task.Delay(5000);
            return new DidYouMean { Words = Summaries.ToList() };
        }
    }
}