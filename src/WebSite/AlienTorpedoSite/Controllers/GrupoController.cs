using Microsoft.AspNetCore.Mvc;
using AlienTorpedoSite.Models.Grupo;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AlienTorpedoSite.ViewModels.Grupo;
using System;

namespace AlienTorpedoSite.Controllers
{
    public class GrupoController : Controller
    {
        public IActionResult Detalhar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Grupo";
            return View();
        }

        public IActionResult Cadastrar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Cadastrar Grupo";
            return View();
        }

        public IActionResult AtrelarEvento(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Atrelar Evento";
            return View();
        }

        public IActionResult btnSorteio(GrupoViewModel grupoViewModel)
        {
            GrupoEvento grupoEvento = new GrupoEvento
            {
                IdGrupoEvento = 18
            };

            //Chamando API para cadastrar o usuário na base 
            using (HttpClient client = new HttpClient())
            {
                //Setando endereço da API
                client.BaseAddress = new System.Uri("http://localhost:53462/api/sorteio");
                //Limpando header
                client.DefaultRequestHeaders.Accept.Clear();
                //Adicionando um novo header do tipo JSON
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Transformando obj Usuario em uma string
                string stringData = JsonConvert.SerializeObject(grupoEvento);

                //Transformando string em um arquivo do tipo JSON
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                //Chamando API passando o arquivo JSON
                HttpResponseMessage response = client.PostAsync("http://localhost:53462/api/sorteio", contentData).Result;

                //Passando retorno da API para uma string
                string retorno = response.Content.ReadAsStringAsync().Result;

                //Fragmentando retorno do arquivo json 
                dynamic resultado = JsonConvert.DeserializeObject(retorno);

                //Enviando retorno para a tela

                ViewBag.grupoEvento = resultado.idGrupoEvento;
                ViewBag.cdGrupo = resultado[0].cdGrupo.Value;
                ViewBag.dtEvento = resultado[0].dtEvento.Value;
                ViewBag.nmEvento = resultado[0].nmEvento.Value;
                ViewBag.nmEndereco = resultado[0].nmEndereco.Value;
                ViewBag.vlEvento = resultado[0].vlEvento.Value;

                GrupoViewModel viewModel = new GrupoViewModel
                {
                    IdGrupoEvento = resultado.idGrupoEvento,
                    cdGrupo = resultado.cdGrupo,
                    dtEvento = resultado.dtEvento,
                    nmEvento = resultado.nmEvento,
                    nmEndereco = resultado.nmEndereco,
                    vlEvento = resultado.vlEvento
                };
                return View(viewModel);
            }
        }
    }
}
