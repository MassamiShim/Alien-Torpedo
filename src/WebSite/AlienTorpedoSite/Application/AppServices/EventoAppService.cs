using AlienTorpedoSite.Application.Interfaces;
using AlienTorpedoSite.Models;
using AlienTorpedoSite.Models.Evento;
using AlienTorpedoSite.Models.Grupo;
using AlienTorpedoSite.Models.Utilidades;
using AlienTorpedoSite.ViewModels.Grupo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var tipoEventos = new List<TipoEvento>();
            try
            {
                HttpClient client = new HttpClient();
                string url = _baseAppService.GetUrl("", "listar_tipoEvento");
                var response = client.GetStringAsync(url);
                tipoEventos = JsonConvert.DeserializeObject<List<TipoEvento>>(response.Result);
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return tipoEventos;
        }

        public string AdicionarTipoEvento(TipoEvento tipoEvento)
        {
            try
            {
                string url = _baseAppService.GetUrl("", "cadastrar_tipoEvento");
                var stringContent = new StringContent(JsonConvert.SerializeObject(tipoEvento), UnicodeEncoding.UTF8, "application/json");
                var response = _http.PostAsync(url, stringContent).Result;

                return response.ToString();
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public List<Evento> ObtemListaEventos()
        {
            var lstEventos = new List<Evento>();

            try
            {
                string url = _baseAppService.GetUrl("", "listar_eventos");
                var response = _http.GetStringAsync(url);
                lstEventos = JsonConvert.DeserializeObject<List<Evento>>(response.Result);
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return lstEventos;
        }

        public string AdicionarEvento(Evento evento)
        {
            try
            {
                string url = _baseAppService.GetUrl("", "cadastrar_evento");
                var stringContent = new StringContent(JsonConvert.SerializeObject(evento), UnicodeEncoding.UTF8, "application/json");
                var response = _http.PostAsync(url, stringContent).Result;

                return response.ToString();
            }
            catch(Exception e)
            {
               throw new ApplicationException(e.Message);
            }
        }

        public GrupoEventoViewModel SortearEvento(GrupoEvento evento)
        {
            var retorno = new GrupoEventoViewModel();

            try
            {
                string url = _baseAppService.GetUrl("", "sortear_evento");
                var stringContent = new StringContent(JsonConvert.SerializeObject(evento), UnicodeEncoding.UTF8, "application/json");

                var response = _http.PostAsync(url, stringContent).Result;
                string json = response.Content.ReadAsStringAsync().Result;
                retorno = JsonConvert.DeserializeObject<GrupoEventoViewModel>(json);
            }
            catch(Exception e)
            {
                new ApplicationException(e.Message);
            }

            return retorno;
        }
    }
}
