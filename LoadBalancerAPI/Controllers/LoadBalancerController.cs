using Microsoft.AspNetCore.Mvc;

namespace LoadBalancerAPI.Controllers
{
    /*
     * Load Balancer calls microservices in the list in an order 
     * based on the used strategy. Returns response back using http.
     */

    //[Route("[action]")]
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
        [Route("search/{query}/{maxAmount}")]
        public async Task<IActionResult> Search(string query, int maxAmount)
            => await ProxyTo(_loadBalancer.NextService() + "/search/" + query + "/" + maxAmount);

        // redirect helper
        private async Task<ContentResult> ProxyTo(string url)
            => Content(await _httpClient.GetStringAsync(url));

        // Load balancer configuration endpoints (quick and dirty no error handling)
        [HttpGet]
        [Route("services/all")]
        public List<string> GetAllService() {
            return _loadBalancer.GetAllServices();
        }

        [HttpPost]
        [Route("services/add/{serviceUrl}")]
        public IActionResult AddService(string serviceUrl) {
            return Accepted(_loadBalancer.AddService(Uri.UnescapeDataString(serviceUrl)));
        }

        [HttpDelete]
        [Route("services/remove/{id}")]
        public ContentResult RemoveService(int id) {
            return Content("id " + _loadBalancer.RemoveService(id) + " removed");
        }

        [HttpGet]
        [Route("strategies/all")]
        public IEnumerable<string> GetAllStrategies() {
            return _loadBalancer.GetAllStrategies().Keys.ToList();
        }

        [HttpGet]
        [Route("strategies/active")]
        public string GetActiveStrategy() {
            return _loadBalancer.GetActiveStrategy().GetType().Name;
        }

        [HttpPut]
        [Route("strategies/active/{strategyName}")]
        public IActionResult SetActiveStrategy(string strategyName) {
            if (strategyName == null)
            {
                return BadRequest();
            }
            _loadBalancer.SetActiveStrategy(strategyName);
            return Content("Active strategy set to " + strategyName);
        }
    }
}