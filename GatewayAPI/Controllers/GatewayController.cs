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

        // Injecting httpClientFactory from app service
        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        [Route("{query}/{maxAmount}")]
        public async Task<IActionResult> Search(string query, int maxAmount)
        {
            return await ProxyTo("https://localhost:44307/search/" + query + "/" + maxAmount);
        }

        private async Task<ContentResult> ProxyTo(string url)
            => Content(await _httpClient.GetStringAsync(url));
    }
}





