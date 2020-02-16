using Microsoft.AspNetCore.Mvc;

namespace AlienTorpedoSite.Controllers
{
    public class TipoEventoController : Controller
    {
        public IActionResult Detalhar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Tipos de Evento";
            return View();
        }

        public IActionResult Cadastrar(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Cadastrar Tipo de Evento";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
