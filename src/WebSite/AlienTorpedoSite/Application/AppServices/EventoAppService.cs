using AlienTorpedoSite.Application.Interfaces;
using AlienTorpedoSite.Models;
using AlienTorpedoSite.Models.Evento;
using AlienTorpedoSite.Models.Grupo;
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
        private readonly HttpClient _http;

        public EventoAppService(BaseAppService baseAppService, HttpClient http)
        {
            _baseAppService = baseAppService;
            _http = http;
        }

        public List<TipoEvento> ObtemTiposEvento()
        {
            HttpClient client = new HttpClient();
            string url = _baseAppService.GetUrlApi() + "api/Evento/ListaTipoEvento";
            var response = client.GetStringAsync(url);
            var tipoEventos = JsonConvert.DeserializeObject<List<TipoEvento>>(response.Result);

            return tipoEventos;
        }

        public string AdicionarTipoEvento(TipoEvento tipoEvento)
        {
            string url = _baseAppService.GetUrlApi() + "api/Evento/CadastraTipoEvento";
            var stringContent = new StringContent(JsonConvert.SerializeObject(tipoEvento), UnicodeEncoding.UTF8, "application/json");
            var response = _http.PostAsync(url, stringContent).Result;

            return response.ToString();
        }

        public List<Evento> ObtemListaEventos()
        {
            HttpClient client = new HttpClient();
            string url = _baseAppService.GetUrlApi() + "api/Evento/ListaEventos";
            var response = client.GetStringAsync(url);
            var lstEventos = JsonConvert.DeserializeObject<List<Evento>>(response.Result);

            return lstEventos;
        }

        public string AdicionarEvento(Evento evento)
        {
            string url = _baseAppService.GetUrlApi() + "api/Evento/CadastraEvento";
            var stringContent = new StringContent(JsonConvert.SerializeObject(evento), UnicodeEncoding.UTF8, "application/json");
            var response = _http.PostAsync(url, stringContent).Result;

            return response.ToString();
        }

        public GrupoEvento SortearEvento(GrupoEvento evento)
        {
            string url = _baseAppService.GetUrlApi() + "api/Sorteio/Sortear";
            var stringContent = new StringContent(JsonConvert.SerializeObject(evento), UnicodeEncoding.UTF8, "application/json");
            var response = _http.PostAsync(url, stringContent).Result;

            var json = response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<GrupoEvento>(json.Result);

            return retorno;
        }
    }
}
