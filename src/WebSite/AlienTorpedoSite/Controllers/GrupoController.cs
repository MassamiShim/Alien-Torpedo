using Microsoft.AspNetCore.Mvc;

namespace AlienTorpedoSite.Controllers
{
    public class GrupoController : Controller
    {
        public IActionResult Detalhar(int Cd_usuario = 0)
        {
            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            ViewData["Title"] = "Grupo";
            return View();
        }

        public IActionResult Cadastrar(int Cd_usuario = 0)
        {
            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            ViewData["Title"] = "Cadastrar Grupo";
            return View();
        }

        public IActionResult AtrelarEvento(int Cd_usuario = 0)
        {
            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            ViewData["Title"] = "Atrelar Evento";
            return View();
        }
    }
}
