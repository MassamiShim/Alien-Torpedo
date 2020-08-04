using AlienTorpedoSite.Models.Grupo;
using AlienTorpedoSite.Models.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlienTorpedoSite.Application.AppServices
{
    public class GrupoAppService
    {
        private readonly BaseAppService _baseAppService;
        private readonly HttpClient _http;

        public GrupoAppService(BaseAppService baseAppService, HttpClient http)
        {
            _baseAppService = baseAppService;
            _http = http;
        }

        public List<Grupo> ListaGrupos()
        {
            HttpClient client = new HttpClient();
            string url = _baseAppService.GetUrlApi() + "api/Grupo/ListaGrupo";
            var response = client.GetStringAsync(url);
            var lstGrupos = JsonConvert.DeserializeObject<List<Grupo>>(response.Result);

            return lstGrupos;
        }

        public string AdicionarGrupo(Grupo grupo)
        {
            string url = _baseAppService.GetUrlApi() + "api/Grupo/CadastraGrupo";
            var stringContent = new StringContent(JsonConvert.SerializeObject(grupo), UnicodeEncoding.UTF8, "application/json");
            var response = _http.PostAsync(url, stringContent).Result;

            return response.ToString();
        }

        public Retorno AtrelarGrupoEvento(GrupoEvento grupo)
        {
            string url = _baseAppService.GetUrlApi() + "api/Grupo/AtrelarGrupoEvento";
            var stringContent = new StringContent(JsonConvert.SerializeObject(grupo), UnicodeEncoding.UTF8, "application/json");
            var response = _http.PostAsync(url, stringContent).Result;

            var json = response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);

            return retorno;
        }

    }
}
