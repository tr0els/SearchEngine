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
        private Dictionary<string, ILoadBalancerStrategy> _strategies;

        public LoadBalancer(ILoadBalancerStrategy strategy)
        {
            _strategy = strategy;
            _services = new List<string>();
            _strategies = new Dictionary<string, ILoadBalancerStrategy>();

            // for easy testing set load balancer services and strategies at instantiation
            if(_services.Count == 0)
            {
                _services.Add("https://localhost:44321");
                _services.Add("https://localhost:44390");
            }

            if(_strategies.Count == 0) 
            {
                _strategies.Add("RoundRobinStrategy", new RoundRobinStrategy());
                _strategies.Add("RandomStrategy", new RandomStrategy());
            }
        }

        public List<string> GetAllServices()
        {
            return _services;
        }

        public int AddService(string url)
        {
            _services.Add(url);
            return _services.Count - 1;
        }
        public int RemoveService(int id)
        {
            _services.RemoveAt(id);
            return id;
        }
        public Dictionary<string, ILoadBalancerStrategy> GetAllStrategies()
        {
            return _strategies;
        }

        public ILoadBalancerStrategy GetActiveStrategy()
        {
            return _strategy;
        }

        public void SetActiveStrategy(string strategyName)
        {
            _strategy = _strategies[strategyName];
        }

        public string NextService()
        {
            return _strategy.NextService(_services);
        }
    }
}
