using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlienTorpedoSite.Application.AppServices;
using AlienTorpedoSite.Models.Grupo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlienTorpedoSite.Controllers
{
    public class SorteioController : Controller
    {
        private readonly EventoAppService _eventoAppService;
        private readonly GrupoAppService _grupoAppService;

        public SorteioController(EventoAppService eventoAppService, GrupoAppService grupoAppService)
        {
            _eventoAppService = eventoAppService;
            _grupoAppService = grupoAppService;
        }

        [HttpGet]
        public IActionResult Sortear()
        {
            var lstGrupos = _grupoAppService.ListaGrupos();
            var lstEventos = _eventoAppService.ObtemListaEventos();

            ViewBag.ListaGrupos = lstGrupos.Select(g => new SelectListItem() { Text = g.NmGrupo, Value = g.CdGrupo.ToString() });
            //ViewBag.ListaEventos = lstEventos.Select(e => new SelectListItem() { Text = e.NmEvento, Value = e.CdEvento.Value.ToString() });

            return View();
        }

        [HttpGet]
        public IActionResult SortearEvento(int CdGrupo, int IdGrupoEvento)
        {
            var evento = new GrupoEvento()
            {
                CdGrupo = CdGrupo,
                IdGrupoEvento = IdGrupoEvento
            };

            var model = _eventoAppService.SortearEvento(evento);    
            
            return PartialView("_DetalheSorteio", model);
        }

        [HttpGet]
        public IActionResult Detalhar()
        {
            List<GrupoEvento> eventos = _eventoAppService.ListarEventosSorteados();
        
            return View(eventos);
        }
    }
}
