using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    public class LoadBalancer
    {
        private List<string> _services;

        int AddService(List<string> services)
        {
            // add services (simple mock list for now)
            _services = services;
            _services.Add("https://localhost:44307/search");
            _services.Add("url to service 2");
        }
        private string GetNextService() // strategy here
        {

        }
    }
}
