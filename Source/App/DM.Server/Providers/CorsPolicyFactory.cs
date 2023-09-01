using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http.Cors;
using DM.AuthServer.Migrations;
using DM.Core;

namespace DM.AuthServer.Providers
{
    public class CorsPolicyFactory : ICorsPolicyProviderFactory
    {
        readonly ICorsPolicyProvider _provider = new AppCorsPolicyProvider();

        public ICorsPolicyProvider GetCorsPolicyProvider(HttpRequestMessage request)
        {
            return _provider;
        }
    }

    public class AppCorsPolicyProvider : ICorsPolicyProvider
    {
        private CorsPolicy _policy;


        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var corsRequestContext = request.GetCorsRequestContext();
            string origin = corsRequestContext.Origin;


            // Create a CORS policy.
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };


            var cors = ConfigurationManager.AppSettings[AppSettingsKey.Cors.GetKey(origin)];


            // Add allowed origins.
            _policy.Origins.Add(cors);

            return Task.FromResult(_policy);
        }
    }
}