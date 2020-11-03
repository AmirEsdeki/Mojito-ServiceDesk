using Mojito.ServiceDesk.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Mojito.ServiceDesk.Infrastructure.Constant
{
    public class ExternalServiceEndPoints : IEndPointAddresses
    {
        private IConfiguration _configuration;

        public ExternalServiceEndPoints(IConfiguration conf)
        {
            _configuration = conf;
            _authenticationURI = _configuration.GetSection("EndPoints").GetSection("Authentication").Value;
            _basicDataURI = _configuration.GetSection("EndPoints").GetSection("BasicData").Value;

        }

        private string _authenticationURI;
        public string AuthenticationURI => _authenticationURI;

        private string _basicDataURI;
        public string BasicDataURI => _basicDataURI;

    }
}
