using Microsoft.AspNetCore.Mvc;
using AlienTorpedoSite.ViewModels.Conta;
using AlienTorpedoSite.Models.Conta;
using System;

namespace AlienTorpedoSite.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult Cadastrar()
        {
            ViewData["Title"] = "Cadastro de Conta";
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ContaViewModel contaViewModel)
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
        

        public IActionResult Entrar()
        {
            ViewData["Title"] = "Entrar";
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(EntrarViewModel entrarViewModel)
        {
            ViewData["Title"] = "Entrar";

            if (ModelState.IsValid)
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

                    return RedirectToAction("Index", "Home", new
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

            return View(entrarViewModel);
        }
        

        public IActionResult Detalhar(int Cd_usuario)
        {
            ViewData["Title"] = "Sua Conta";

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


        public IActionResult Editar(int Cd_usuario)
        {
            ViewData["Title"] = "Edição de Conta";

            //Chamar API para receber os dados do usuario

            //Passar o retorno da API para o objeto ContaViewModel
            ContaViewModel contaViewModel = new ContaViewModel
            {
                Cd_usuario = Cd_usuario,
                Nm_email = "frank@gmail.com",
                Nm_usuario = "Frank Wesley",
                Nm_senha = "123",
                Dv_ativo = true,
                Dt_inclusao = DateTime.UtcNow
            };

            //verificando se retornou
            if (contaViewModel == null)
                return NotFound();

            return View(contaViewModel);
        }

        [HttpPost]
        public IActionResult Editar(int Cd_usuario, ContaViewModel contaViewModel)
        {
            if (ModelState.IsValid)
            {
                //atribuindo informações editadas no objeto usuario
                Usuario usuario = new Usuario
                {
                    Cd_usuario = contaViewModel.Cd_usuario,
                    Nm_email = contaViewModel.Nm_email,
                    Nm_senha = contaViewModel.Nm_senha,
                    Nm_usuario = contaViewModel.Nm_usuario,
                    Dv_ativo = contaViewModel.Dv_ativo,
                    Dt_inclusao = contaViewModel.Dt_inclusao //formato padrão
                };

                //Chamar API para editar o usuário aqui e enviar os parametros necessários

                //receber retorno e mostrar na tela
                bool retorno_API = true;

                //redirecionar para outra tela, se sucesso
                if (retorno_API)
                {
                    //sucesso
                    ViewBag.Mensagem = "Alterações realizadas com sucesso!";
                    ViewBag.Codigo = 0;

                    //adicionar pop-up informando que o cadastro foi realizado com sucesso e somente depois redirecionar para a tela de login
                    return RedirectToAction("Detalhar", "Conta", new
                    {
                        Cd_usuario = usuario.Cd_usuario
                    });
                }
                else
                {
                    ViewBag.Mensagem = "Alterações não realizadas!";
                    ViewBag.Codigo = 1;
                }
            }
            else
            {
                //erro
                ViewBag.Mensagem = "Alterações não estão validas";
                ViewBag.Codigo = 1;
            }

            return View(contaViewModel);
        }


        public IActionResult Cancelar(int Cd_usuario)
        {
            //Chamar API para cancelar o usuário e passar parametros necessários

            bool retorno_API = true;
            //verificar retorno da API

            if (retorno_API)
            {
                ViewBag.Mensagem = "Conta cancelada com sucesso!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Cd_codigo = 1;
                ViewBag.Mensagem = "Erro no cancelamento da conta!";
            }

            return View();
        }


        public IActionResult Sair()
        {
            return RedirectToAction("Index","Home");
        }

    }
}
