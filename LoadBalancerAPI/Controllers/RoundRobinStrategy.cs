namespace LoadBalancerAPI.Controllers
{
    public class RoundRobinStrategy : ILoadBalancerStrategy
    {
        private int _nextServiceId;

        public string NextService(List<string> services)
        {
            // round robin logic for finding next service in list
            return services[_nextServiceId++ % services.Count];
        }
    }
}