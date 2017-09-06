using Microsoft.AspNetCore.Mvc;
using AlienTorpedoSite.ViewModels.Home;

namespace AlienTorpedoSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                //chamar API
                //receber retorno e mostrar na tela
                //redirecionar para outra tela, se sucesso

                ViewBag.Mensagem = "Acesso liberado!";
                ViewBag.Codigo = 0;
            }
            else
            {
                //erro
                ViewBag.Mensagem = "Dados Invalidos!";
                ViewBag.Codigo = 1;
            }

            return View(viewModel);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
