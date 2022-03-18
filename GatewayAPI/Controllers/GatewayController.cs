using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GatewayAPI.Controllers
{
    [Route("[action]")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpGet]
        public async Task<IActionResult> Books()
            => await ProxyTo("https://localhost:44388/books");

        /*
        [HttpGet]
        public async Task<IActionResult> Authors()
            => await ProxyTo("https://localhost:44307/authors");
        */

        private async Task<ContentResult> ProxyTo(string url)
            => Content(await _httpClient.GetStringAsync(url));
    }






    /*
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        //private static SearchLogic searchLogic = new SearchLogic(new Database());

        // GET: api/<GatewayController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GatewayController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GatewayController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GatewayController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GatewayController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    */
}





