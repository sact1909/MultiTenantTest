namespace MultiTenantTest.Entities
{
    public class TenantSettings
    {
        public IEnumerable<Tenant> Tenants { get; set; }
    }

    public class Tenant 
    {
        public string Id { get; set; }
        public string ConnectionString { get; set; }
    }
}
