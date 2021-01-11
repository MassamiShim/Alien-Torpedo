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
using System.Threading.Tasks;

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
            //var lstGrupos = _grupoAppService.ListaGrupos();
            //var lstEventos = _eventoAppService.ObtemListaEventos();

            //ViewBag.ListaGrupos = lstGrupos.Select(g => new SelectListItem() { Text = g.NmGrupo, Value = g.CdGrupo.ToString() });
            //ViewBag.ListaEventos = lstEventos.Select(e => new SelectListItem() { Text = e.NmEvento, Value = e.CdEvento.ToString() });

            Util carregamentoCombo = ObtemComboGruposEventos();
            ViewBag.ListaGrupos = carregamentoCombo.ListaGrupos;
            ViewBag.ListaEventos = carregamentoCombo.ListaEventos;

            //object cdRetorno;
            //object strRetorno;
            //TempData.TryGetValue("Codigo", out cdRetorno);
            //TempData.TryGetValue("Mensagem", out strRetorno);

            //ViewBag.Codigo = cdRetorno != null ? (int)cdRetorno : 0;
            //ViewBag.Mensagem = strRetorno != null ? strRetorno.ToString() : null;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AtrelarEvento(GrupoEvento grupo)
        {
            //Recarrega os combos
            Util carregamentoCombo = ObtemComboGruposEventos();

            if (!ModelState.IsValid)
            {    
                ViewBag.ListaGrupos = carregamentoCombo.ListaGrupos;
                ViewBag.ListaEventos = carregamentoCombo.ListaEventos;
                
                return View(grupo);
            }
                
            var retorno = await _grupoAppService.AtrelarGrupoEvento(grupo);
            
            if(retorno != null)
            {
                ViewBag.Codigo = retorno.cdretorno;
                ViewBag.Mensagem = retorno.mensagem;
                ViewBag.ListaGrupos = carregamentoCombo.ListaGrupos;
                ViewBag.ListaEventos = carregamentoCombo.ListaEventos;

                TempData["Mensagem"] = retorno.mensagem;
                TempData["Codigo"] = retorno.cdretorno;

                if (retorno.cdretorno == 1)
                    return View(grupo);
            }
          
            return RedirectToAction("AtrelarEvento");

        }

        private Util ObtemComboGruposEventos()
        {
            Util util = new Util();
            var lstGrupos = _grupoAppService.ListaGrupos();
            var lstEventos = _eventoAppService.ObtemListaEventos();
            
            util.ListaGrupos = lstGrupos.Select(g => new SelectListItem() { Text = g.NmGrupo, Value = g.CdGrupo.ToString() });
            util.ListaEventos = lstEventos.Select(e => new SelectListItem() { Text = e.NmEvento, Value = e.CdEvento.ToString() });

            return util;
        }


       
    }
}
