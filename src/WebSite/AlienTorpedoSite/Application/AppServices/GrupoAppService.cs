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
            var lstGrupos = new List<Grupo>();

            try
            {
                string url = _baseAppService.GetUrl("", "listar_grupos");
                var response = _http.GetStringAsync(url);
                lstGrupos = JsonConvert.DeserializeObject<List<Grupo>>(response.Result);
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return lstGrupos;
        }

        public string AdicionarGrupo(Grupo grupo)
        {
            try
            {
                string url = _baseAppService.GetUrl("", "cadastrar_grupos");
                var stringContent = new StringContent(JsonConvert.SerializeObject(grupo), UnicodeEncoding.UTF8, "application/json");
                var response = _http.PostAsync(url, stringContent).Result;

                return response.ToString();
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public Retorno AtrelarGrupoEvento(GrupoEvento grupo)
        {
            var retorno = new Retorno();

            try
            {
                string url = _baseAppService.GetUrl("", "vincular_GrupoEvento");
                var stringContent = new StringContent(JsonConvert.SerializeObject(grupo), UnicodeEncoding.UTF8, "application/json");
                var response = _http.PostAsync(url, stringContent).Result;

                var json = response.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);
            }
            catch(Exception e)
            {
                retorno.cdretorno = 1;
                retorno.mensagem = "Erro ao tentar atrelar grupo a evento.";
                throw new ApplicationException(e.Message);
            }

            return retorno;
        }

    }
}
