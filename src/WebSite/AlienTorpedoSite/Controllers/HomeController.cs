using Microsoft.AspNetCore.Mvc;

namespace AlienTorpedoSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int Cd_usuario = 0)
        {
            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            return View();
        }

        public IActionResult Sobre(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Sobre";
            ViewData["Message"] = "Sobre nosso sistema:";

            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            return View();
        }

        public IActionResult Contato(int Cd_usuario = 0)
        {
            ViewData["Title"] = "Contato";
            ViewData["Message"] = "Entre em contato conosco:";

            if (Cd_usuario != 0)
                ViewBag.Cd_usuario = Cd_usuario;

            return View();
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
