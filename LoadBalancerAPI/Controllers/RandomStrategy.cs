namespace LoadBalancerAPI.Controllers
{
    public class RandomStrategy : ILoadBalancerStrategy
    {
        Random rnd = new Random();
        public string NextService(List<string> services)
        {
            // random logic for finding next service in list
            return services[rnd.Next(services.Count)];
        }
    }
}