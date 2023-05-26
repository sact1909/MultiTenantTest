using Microsoft.Extensions.Options;

namespace MultiTenantTest.Entities
{
    public class TenantProvider
    {
        private const string TenantIdHeaderName = "X-TenantId";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TenantSettings _tenantSettings;

        public TenantProvider(IHttpContextAccessor httpContextAccessor, IOptions<TenantSettings> tenantSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantSettings = tenantSettings.Value;
        }

        public string Tenantid => _httpContextAccessor.HttpContext.Request.Headers[TenantIdHeaderName].ToString();

        public string GetConnectionString()
        {
            var tenant = _tenantSettings.Tenants.FirstOrDefault(t => t.Id == Tenantid);
            return tenant?.ConnectionString;
        }   
    }
}
