using AlienTorpedoSite.Application.AppServices;
using AlienTorpedoSite.Models.Evento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace AlienTorpedoSite.Controllers
{
    public class EventoController : Controller
    {
        private readonly EventoAppService _eventoAppService;

        public EventoController(EventoAppService eventoAppService)
        {
            _eventoAppService = eventoAppService;
        }

        public IActionResult Detalhar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Eventos";
            var lstEventos = _eventoAppService.ObtemListaEventos();

            return View(lstEventos);
        }


        public IActionResult Cadastrar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Cadastrar Evento";
            var lstTipoEventos = _eventoAppService.ObtemTiposEvento();
            ViewBag.TiposEvento = lstTipoEventos.Select(c => new SelectListItem(){ Text = c.NmTipoEvento, Value = c.CdTipoEvento.ToString() }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Evento evento)
        {
            if (!ModelState.IsValid)
                return View(evento);

            string strRetorno = _eventoAppService.AdicionarEvento(evento);
            return RedirectToAction("Detalhar");
        }

    }
}
