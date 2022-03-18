using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    /*
     * Gateway calls microservices and returns response back using http
     */

    [Route("[action]")]
    [ApiController]

    public class ProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly LoadBalancer _loadBalancer;

        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            // create a client using httpClientFactory injected by app service
            _httpClient = httpClientFactory.CreateClient();


            _loadBalancer = new LoadBalancer(services);
        }

        [HttpGet]
        [Route("{query}/{maxAmount}")]
        public async Task<IActionResult> Search(string query, int maxAmount)
        {
            return await ProxyTo("https://localhost:44307/search/" + query + "/" + maxAmount);
        }

        /*
        [HttpGet]
        public async Task<IActionResult> Authors()
            => await ProxyTo("https://localhost:44307/authors");
        */

        private async Task<ContentResult> ProxyTo(string url) 
        {
            return Content(await _httpClient.GetStringAsync(url));
        }
        
        private async string LoadBalanceTo(string url)
        {
            var service = _loadBalancer.GetNextService(url);
            return ProxyTo(url);
        }
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





