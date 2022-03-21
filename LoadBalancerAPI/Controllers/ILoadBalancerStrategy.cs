namespace LoadBalancerAPI.Controllers
{
    public interface ILoadBalancerStrategy
    {
        public string NextService(List<string> services);
    }
}
