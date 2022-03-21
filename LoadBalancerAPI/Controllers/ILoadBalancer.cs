namespace LoadBalancerAPI.Controllers
{
    public interface ILoadBalancer
    {
        public string NextService();
    }
}
