using Microsoft.AspNetCore.Mvc;
using AlienTorpedoSite.Models.Grupo;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AlienTorpedoSite.ViewModels.Grupo;
using System;
using AlienTorpedoSite.Application.AppServices;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlienTorpedoSite.Controllers
{
    public class GrupoController : Controller
    {
        private readonly GrupoAppService _grupoAppService;
        private readonly EventoAppService _eventoAppService;

        public GrupoController(GrupoAppService grupoAppService, EventoAppService eventoAppService)
        {
            _grupoAppService = grupoAppService;
            _eventoAppService = eventoAppService;
        }

        public IActionResult Detalhar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Grupo";
            var lstGrupos = _grupoAppService.ListaGrupos();

            return View(lstGrupos);
        }

        public IActionResult Cadastrar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Cadastrar Grupo";
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(Grupo grupo)
        {
            if(!ModelState.IsValid)
                return View();

            string strRetorno = _grupoAppService.AdicionarGrupo(grupo);
            return RedirectToAction("Detalhar");
        }

        public IActionResult AtrelarEvento()
        {      
            var lstGrupos = _grupoAppService.ListaGrupos();
            var lstEventos = _eventoAppService.ObtemListaEventos();

            ViewBag.ListaGrupos = lstGrupos.Select(g => new SelectListItem() { Text = g.NmGrupo, Value = g.CdGrupo.ToString() });
            ViewBag.ListaEventos = lstEventos.Select(e => new SelectListItem() { Text = e.NmEvento, Value = e.CdEvento.ToString() });

            object cdRetorno;
            object strRetorno;
            TempData.TryGetValue("Codigo", out cdRetorno);
            TempData.TryGetValue("Mensagem", out strRetorno);

            ViewBag.Codigo = cdRetorno != null ? (int)cdRetorno : 0;
            ViewBag.Mensagem = strRetorno != null ? strRetorno.ToString() : null;

            return View();
        }

        [HttpPost]
        public IActionResult AtrelarEvento(GrupoEvento grupo)
        {
            if(!ModelState.IsValid)
                return View(grupo);

            var retorno = _grupoAppService.AtrelarGrupoEvento(grupo);
            
            if(retorno != null)
            {
                ViewBag.Codigo = retorno.cdretorno;
                ViewBag.Mensagem = retorno.mensagem;

                TempData["Mensagem"] = retorno.mensagem;
                TempData["Codigo"] = retorno.cdretorno;

                if (retorno.cdretorno == 1)
                    return View(grupo);

            }
          
            return RedirectToAction("AtrelarEvento");

        }

        public IActionResult btnSorteio(GrupoViewModel grupoViewModel)
        {
            GrupoEvento grupoEvento = new GrupoEvento
            {
                IdGrupoEvento = 1
            };

            //Chamando API para cadastrar o usuário na base 
            using (HttpClient client = new HttpClient())
            {
                //Setando endereço da API
                client.BaseAddress = new System.Uri("http://localhost:51889/api/sorteio");
                //Limpando header
                client.DefaultRequestHeaders.Accept.Clear();
                //Adicionando um novo header do tipo JSON
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Transformando obj Usuario em uma string
                string stringData = JsonConvert.SerializeObject(grupoEvento);

                //Transformando string em um arquivo do tipo JSON
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                //Chamando API passando o arquivo JSON
                HttpResponseMessage response = client.PostAsync("http://localhost:51889/api/sorteio", contentData).Result;

                //Passando retorno da API para uma string
                string retorno = response.Content.ReadAsStringAsync().Result;

                //Fragmentando retorno do arquivo json 
                dynamic resultado = JsonConvert.DeserializeObject(retorno);

                //Enviando retorno para a tela

                ViewBag.grupoEvento = resultado.resultado[0].IdGrupoEvento;
                ViewBag.cdGrupo = resultado.resultado[0].CdGrupo;
                ViewBag.dtEvento = resultado.resultado[0].DtEvento;
                ViewBag.nmEvento = resultado.resultado[0].NmEvento;
                ViewBag.nmEndereco = resultado.resultado[0].NmEndereco;
                ViewBag.vlEvento = resultado.resultado[0].VlEvento;

                GrupoViewModel viewModel = new GrupoViewModel
                {
                    IdGrupoEvento = resultado.resultado[0].IdGrupoEvento,
                    cdGrupo = resultado.resultado[0].CdGrupo,
                    dtEvento = resultado.resultado[0].DtEvento,
                    nmEvento = resultado.resultado[0].NmEvento,
                    nmEndereco = resultado.resultado[0].NmEndereco,
                    vlEvento = resultado.resultado[0].VlEvento
                };
                return View(viewModel);
            }
        }
    }
}
