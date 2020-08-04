using AlienTorpedoSite.Application.AppServices;
using AlienTorpedoSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienTorpedoSite.Controllers
{
      public class TipoEventoController : Controller
      {

        private readonly EventoAppService _eventoAppService;

        public TipoEventoController(EventoAppService eventoAppService)
        {
            _eventoAppService = eventoAppService;
        }
        public IActionResult Detalhar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Tipos de Evento";
            var lstTipoEventos = _eventoAppService.ObtemTiposEvento();

            return View(lstTipoEventos);
        }

        public IActionResult Cadastrar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Cadastrar Tipo de Evento";
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(TipoEvento tipoEvento)
        {
            if (!ModelState.IsValid)
                return View(tipoEvento);

            string srtRetorno = _eventoAppService.AdicionarTipoEvento(tipoEvento);
            return RedirectToAction("Detalhar");

        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
