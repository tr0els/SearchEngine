namespace LoadBalancerAPI.Controllers
{
    public interface ILoadBalancer
    {
        public List<string> GetAllServices();
        public int AddService(string url);
        public int RemoveService(int id);
        public Dictionary<string, ILoadBalancerStrategy> GetAllStrategies();
        public ILoadBalancerStrategy GetActiveStrategy();
        public void SetActiveStrategy(string strategyName);
        public string NextService();
    }
}
