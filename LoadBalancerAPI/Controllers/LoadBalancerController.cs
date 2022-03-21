using Microsoft.AspNetCore.Mvc;

namespace LoadBalancerAPI.Controllers
{
    /*
     * Load Balancer calls microservices in the list in an order 
     * based on the used strategy. Returns response back using http.
     */

    [Route("[action]")]
    [ApiController]

    public class LoadBalancerController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private LoadBalancer _loadBalancer;

        public LoadBalancerController(IHttpClientFactory httpClientFactory, LoadBalancer loadBalancer)
        {
            // create a client using httpClientFactory injected by app service
            _httpClient = httpClientFactory.CreateClient();
            
            // use singleton loadbalancer instance injected by app service
            _loadBalancer = loadBalancer;
        }

        [HttpGet]
        [Route("{query}/{maxAmount}")]
        public async Task<IActionResult> Search(string query, int maxAmount)
            => await ProxyTo(_loadBalancer.NextService() + "/search/" + query + "/" + maxAmount);

        private async Task<ContentResult> ProxyTo(string url)
            => Content(await _httpClient.GetStringAsync(url));

        // Load balancer configuration endpoints
        [HttpGet]
        [Route("{ServiceUrl}")]
        public ContentResult AddService(string serviceUrl) {
            return Content(_loadBalancer.AddService(serviceUrl)+"");
        }
    }
}