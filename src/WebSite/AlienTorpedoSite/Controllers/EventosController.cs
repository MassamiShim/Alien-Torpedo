using Microsoft.AspNetCore.Mvc;

namespace AlienTorpedoSite.Controllers
{
    public class EventosController : Controller
    {
        public IActionResult Eventos(int Cd_usuario = 0)
        {
            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            ViewData["Title"] = "Eventos";
            return View();
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
