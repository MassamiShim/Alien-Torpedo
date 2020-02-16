using AlienTorpedoSite.Application.Interfaces;
using AlienTorpedoSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlienTorpedoSite.Application.AppServices
{
    public class EventoAppService : IEvento
    {
        private readonly BaseAppService _baseAppService;

        public EventoAppService(BaseAppService baseAppService)
        {
            _baseAppService = baseAppService;             
        }

        public List<TipoEvento> ObtemTiposEvento()
        {
            HttpClient client = new HttpClient();
            //string url = "http://localhost:53113/api/Evento/ListaTipoEvento";
            string url = _baseAppService.GetUrlApi() + "api/Evento/ListaTipoEvento";
            var response = client.GetStringAsync(url);
            var tipoEventos = JsonConvert.DeserializeObject<List<TipoEvento>>(response.Result);

            return tipoEventos;
        }
    }
}
