namespace EServi.Consul
{
    public class ConsulOptions
    {
        public string HttpEndpoint { get; set; }
        public string ServiceName { get; set; }
        public string ServiceHost { get; set; }
        public int ServicePort { get; set; }
    }
}