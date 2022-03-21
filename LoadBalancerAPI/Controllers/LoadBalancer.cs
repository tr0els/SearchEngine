using Microsoft.AspNetCore.Mvc;

namespace LoadBalancerAPI.Controllers
{
    /*
     * Using strategy pattern for the possibillity to use
     * different load balancer strategies. A strategy can 
     * be set using constructor or changed at runtime using 
     * the setter.
     * 
     * State for currently selected service is added, 
     * in case a strategy algorithm needs to use it.
     * 
     * Services can be added or removed during runtime.
     */
    public class LoadBalancer
    {
        private ILoadBalancerStrategy _strategy;
        private List<string> _services;

        public LoadBalancer(ILoadBalancerStrategy strategy)
        {
            _services = new List<string>();
            _strategy = strategy;

            // for dev purposes - add a service to load balancer if list is empty
            if(_services.Count == 0)
            {
                this.AddService("https://localhost:44307");
            } 
        }

        public int AddService(string url)
        {
            _services.Add(url);
            return _services.Count;
        }
        public void RemoveService(int id)
        {
            _services.RemoveAt(id);
        }

        public void SetStrategy(ILoadBalancerStrategy strategy)
        {
            _strategy = strategy;
        }

        public string NextService()
        {
            return _strategy.NextService(_services);
        }
    }
}
