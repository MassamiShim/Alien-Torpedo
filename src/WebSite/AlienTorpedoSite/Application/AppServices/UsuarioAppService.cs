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
            Retorno retorno = new Retorno();

            try
            {
                string url = _baseAppService.GetUrl("", "cadastrar_usuario");
                var stringContent = new StringContent(JsonConvert.SerializeObject(usuario), UnicodeEncoding.UTF8, "application/json");
                var response = _http.PostAsync(url, stringContent).Result;

                var json = response.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);

            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
            
            return retorno;
        }

        public Retorno EditarUsuario(Usuario usuario)
        {
            Retorno retorno = new Retorno();

            try
            {
                string url = _baseAppService.GetUrl("", "editar_usuario");
                var stringContent = new StringContent(JsonConvert.SerializeObject(usuario), UnicodeEncoding.UTF8, "application/json");
                var response = _http.PutAsync(url, stringContent).Result;

                var json = response.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return retorno;            
        }

        public Retorno AutentificarUsuario(string NmEmail, string NmSenha)
        {
            Retorno retorno = new Retorno();

            try
            {
                string url = _baseAppService.GetUrl("", "autenticar_usuario");
                url = url.Replace("{{email}}", NmEmail).Replace("{{senha}}", NmSenha);

                var response = _http.GetAsync(url).Result;

                var json = response.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);
            }
            catch(Exception)
            {
                retorno.cdretorno = 1;
                retorno.mensagem = "Não foi possível realizar a autentificação. Tente mais tarde!";
            }

            return retorno;
        }

        public Retorno AlterarStatusUsuario(Usuario usuario)
        {
            Retorno retorno = new Retorno();

            try
            {
                string url = _baseAppService.GetUrl("", "alterarStatus_usuario");
                var stringContent = new StringContent(JsonConvert.SerializeObject(usuario), UnicodeEncoding.UTF8, "application/json");
                var response = _http.PutAsync(url, stringContent).Result;

                var json = response.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<Retorno>(json.Result);
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return retorno;
        }
    }
}
