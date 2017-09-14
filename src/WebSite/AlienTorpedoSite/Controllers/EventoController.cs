using Microsoft.AspNetCore.Mvc;

namespace AlienTorpedoSite.Controllers
{
    public class EventoController : Controller
    {
        public IActionResult Detalhar(int Cd_usuario = 0)
        {
            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            ViewData["Title"] = "Eventos";
            return View();
        }

        public IActionResult Cadastrar(int Cd_usuario = 0)
        {
            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            ViewData["Title"] = "Cadastrar Evento";
            return View();
        }
    }
}
