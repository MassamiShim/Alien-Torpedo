using AlienTorpedoSite.Models.Utilidades;
using AlienTorpedoSite.Models.Conta;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlienTorpedoSite.Application.AppServices
{
    public class UsuarioAppService
    {
        private readonly BaseAppService _baseAppService;
        private readonly HttpClient _http;

        public UsuarioAppService(BaseAppService baseApp, HttpClient http)
        {
            _baseAppService = baseApp;
            _http = http;
        }

        public Retorno AdicionarUsuario(Usuario usuario)
        {
            string url = _baseAppService.GetUrlApi() + "api/Usuario/CadastraUsuario";
            var stringContent = new StringContent(JsonConvert.SerializeObject(usuario), UnicodeEncoding.UTF8, "application/json");
            var response = _http.PostAsync(url, stringContent).Result;

            var json = response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);

            return retorno;
        }

        public Retorno EditarUsuario(Usuario usuario)
        {
            string url = _baseAppService.GetUrlApi() + "api/Usuario/EditaUsuario";
            var stringContent = new StringContent(JsonConvert.SerializeObject(usuario), UnicodeEncoding.UTF8, "application/json");
            var response = _http.PutAsync(url, stringContent).Result;

            var json = response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);

            return retorno;            
        }

        public Retorno AutentificarUsuario(string NmEmail, string NmSenha)
        {
            string url = _baseAppService.GetUrlApi() + String.Format("api/Usuario/AutenticarUsuario?NmEmail={0}&NmSenha={1}", NmEmail, NmSenha);
            var response = _http.GetAsync(url).Result;

            var json = response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);

            return retorno;
        }

        public Retorno AlterarStatusUsuario(Usuario usuario)
        {
            string url = _baseAppService.GetUrlApi() + "api/Usuario/AlteraStatus";
            var stringContent = new StringContent(JsonConvert.SerializeObject(usuario), UnicodeEncoding.UTF8, "application/json");
            var response = _http.PutAsync(url, stringContent).Result;

            var json = response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);

            return retorno;
        }
    }
}
