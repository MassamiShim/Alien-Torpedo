using Microsoft.AspNetCore.Mvc;

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
    }
}
