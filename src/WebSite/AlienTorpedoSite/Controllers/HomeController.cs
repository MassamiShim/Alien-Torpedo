using Microsoft.AspNetCore.Mvc;
using AlienTorpedoSite.ViewModels.Home;
using AlienTorpedoSite.Models;
using System;

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

        public IActionResult Entrar()
        {
            ViewData["Title"] = "Entrar";
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(EntrarViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                //chamar API aqui para validar o login 

                //receber retorno da API

                //recebendo dados da API e redirecionando para a tela Conta
                Usuario usuario = new Usuario
                {
                    //alterar aqui embaixo para receber os dados da API
                    Cd_usuario = 1,
                    Dv_ativo = true,
                };

                //redirecionar para outra tela, se sucesso
                if (usuario.Dv_ativo == true)
                {
                    ViewBag.Mensagem = "Acesso liberado!";
                    ViewBag.Codigo = 0;

                    return RedirectToAction("Conta", "Home", new
                    {
                        Cd_usuario = usuario.Cd_usuario
                    });
                }
                else
                {
                    ViewBag.Mensagem = "Acesso não disponível!";
                    ViewBag.Codigo = 1;
                }

            }
            else
            {
                //erro
                ViewBag.Mensagem = "Dados Invalidos!";
                ViewBag.Codigo = 1;
            }

            return View(viewModel);
        }

        public IActionResult Conta(int Cd_usuario)
        {
            //chamar API para receber dados do usuário solicitado

            //passando dados para a viewModel para em seguida ser apresentado em tela
            ContaViewModel contaViewModel = new ContaViewModel
            {
                Cd_usuario = Cd_usuario,
                Nm_email = "frank@gmail.com",
                Nm_usuario = "Frank Wesley",
                Dv_ativo = true,
                Dt_inclusao = DateTime.UtcNow
            };

            ViewData["Title"] = "Sua Conta";
            ViewBag.Cd_usuario = Cd_usuario;
            return View(contaViewModel);
        }

        public IActionResult Cadastro()
        {
            ViewData["Title"] = "Cadastro";
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContaViewModel contaViewModel)
        {
            if (ModelState.IsValid)
            {
                //atribuindo informações cadastradas ao objeto usuario
                Usuario usuario = new Usuario
                {
                    Nm_email = contaViewModel.Nm_email,
                    Nm_senha = contaViewModel.Nm_senha,
                    Nm_usuario = contaViewModel.Nm_usuario,
                    Dv_ativo = true,
                    Dt_inclusao = DateTime.UtcNow //formato padrão
                };

                //Chamar API para cadastrar o usuário aqui e enviar os parametros necessários

                //receber retorno e mostrar na tela
                bool retorno_API = true;

                //redirecionar para outra tela, se sucesso
                if (retorno_API)
                {
                    //sucesso
                    ViewBag.Mensagem = "Cadastro Realizado com sucesso!";
                    ViewBag.Codigo = 0;

                    //adicionar pop-up informando que o cadastro foi realizado com sucesso e somente depois redirecionar para a tela de login
                    return RedirectToAction("Entrar");
                }
                else
                {
                    ViewBag.Mensagem = "Login não realizado!";
                    ViewBag.Codigo = 1;
                }
            }
            else
            {
                //erro
                ViewBag.Mensagem = "Cadastro não está valido!";
                ViewBag.Codigo = 1;
            }

            return View(contaViewModel);
        }

        public IActionResult Sair()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
