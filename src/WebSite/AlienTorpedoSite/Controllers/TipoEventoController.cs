using Microsoft.AspNetCore.Mvc;

namespace AlienTorpedoSite.Controllers
{
    public class TipoEventoController : Controller
    {
        public IActionResult Detalhar(int Cd_usuario = 0)
        {
            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            ViewData["Title"] = "Tipos de Evento";
            return View();
        }

        public IActionResult Cadastrar(int Cd_usuario = 0)
        {
            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            ViewData["Title"] = "Cadastrar Tipo de Evento";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
