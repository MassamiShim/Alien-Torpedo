using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlienTorpedoSite.Application.AppServices
{
    public class BaseAppService
    {
        public readonly IConfiguration _configuration;

        public BaseAppService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetUrl(string externalAPI, string routeName)
        {
            string url = string.Empty;
            string route = string.Empty;

            try
            {
                externalAPI = string.IsNullOrWhiteSpace(externalAPI) ? "ExternalAlienAPI" : externalAPI;
                var routes = _configuration.GetSection(externalAPI).GetChildren().ToDictionary(x => x.Key, y => y.Value);

                routes.TryGetValue("URL_BaseAPI", out url);
                routes.TryGetValue(routeName, out route);

            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return url + route;
                       
        }


    }
}
