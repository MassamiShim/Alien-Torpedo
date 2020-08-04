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

        public string GetUrlApi()
        {
            string urlApi = _configuration.GetValue<string>("Url_AlienTorpedoAPI");
            return urlApi;
        }
    }
}
