using Microsoft.AspNetCore.Mvc;

namespace AlienTorpedoSite.Controllers
{
    public class EventoController : Controller
    {
        public IActionResult Detalhar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Eventos";
            return View();
        }

        public IActionResult Cadastrar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Cadastrar Evento";
            return View();
        }
    }
}
