using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlienTorpedoSite.Application.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace AlienTorpedoSite.Controllers
{ 
    public class HomeController : Controller
    {
        private readonly EventoAppService _eventoAppService;

        public HomeController(EventoAppService eventoAppService)
        {
            _eventoAppService = eventoAppService;
        }

        public JsonResult Teste()
        {
            var retorno = _eventoAppService.ObtemTiposEvento();
            return Json(retorno);
        }

        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Sobre()
        {
            ViewData["Title"] = "Sobre";
            ViewData["Message"] = "Sobre nosso sistema:";

            if (HttpContext.Session.GetString("Cd_usuario") != null)
                ViewBag.Dv_logado = true;

            return View();
        }

        public IActionResult Contato()
        {
            ViewData["Title"] = "Contato";
            ViewData["Message"] = "Entre em contato conosco:";
            
            if (HttpContext.Session.GetString("Cd_usuario") != null)
                ViewBag.Dv_logado = true;

            return View();
        }
        
        public IActionResult Error(string msg)
        {
            ViewBag.Mensagem = msg;
            return View();
        }
    }
}
