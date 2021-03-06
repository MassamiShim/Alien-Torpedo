﻿using Microsoft.AspNetCore.Mvc;
using AlienTorpedoSite.ViewModels.Conta;
using AlienTorpedoSite.Models.Conta;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AlienTorpedoSite.Application.AppServices;

namespace AlienTorpedoSite.Controllers
{
    public class ContaController : Controller
    {
        private readonly UsuarioAppService _usuarioAppService;

        public ContaController(UsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

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
                Usuario usuario = new Usuario
                {
                    NmEmail = contaViewModel.Nm_email,
                    NmSenha = contaViewModel.Nm_senha,
                    NmUsuario = contaViewModel.Nm_usuario,
                    DvAtivo = true,
                };

                var retorno = _usuarioAppService.AdicionarUsuario(usuario);
                ViewBag.Codigo = retorno.cdretorno;
                ViewBag.Mensagem = retorno.mensagem;

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

                var retorno = _usuarioAppService.AutentificarUsuario(entrarViewModel.Nm_email, entrarViewModel.Nm_senha);

                ViewBag.Codigo = retorno.cdretorno;
                ViewBag.Mensagem = retorno.mensagem;

                //se sucesso redireciono
                if (retorno.cdretorno == 0 && retorno.usuario != null)
                {                    
                    //Armazenando dados do usuário em sessão
                    HttpContext.Session.SetString("Cd_usuario", JsonConvert.SerializeObject(retorno.usuario));                    
                    return RedirectToAction("Index", "Home");
                }                   
            }
            else
            {
                //erro
                ViewBag.Mensagem = "Dados Inválidos!";
                ViewBag.Codigo = 1;
            }

            return View(entrarViewModel);
        }


        public IActionResult Detalhar()
        {
            ViewData["Title"] = "Sua Conta";

            //passando dados da sessão para a viewModel exibir em tela
            var currentUser = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cd_usuario"));

            ContaViewModel contaViewModel = new ContaViewModel
            {
                Cd_usuario = currentUser.CdUsuario,
                Nm_email = currentUser.NmEmail,
                Nm_usuario = currentUser.NmUsuario,
                Dv_ativo = currentUser.DvAtivo,
                Dt_inclusao = currentUser.DtInclusao
            };

            ViewData["Title"] = "Sua Conta";
            return View(contaViewModel);
        }


        public IActionResult Editar()
        {
            ViewData["Title"] = "Edição de Conta";
            
            //passando dados da sessão para a viewModel exibir em tela        
            var currentUser = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cd_usuario"));

            ContaViewModel contaViewModel = new ContaViewModel
            {
                Cd_usuario = currentUser.CdUsuario,
                Nm_email = currentUser.NmEmail,
                Nm_usuario = currentUser.NmUsuario,
                Dv_ativo = currentUser.DvAtivo,
                Dt_inclusao = currentUser.DtInclusao
            };

            return View(contaViewModel);
        }

        [HttpPost]
        public IActionResult Editar(ContaViewModel contaViewModel)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario
                {
                    CdUsuario = contaViewModel.Cd_usuario,
                    NmEmail = contaViewModel.Nm_email,
                    NmSenha = contaViewModel.Nm_senha,
                    NmUsuario = contaViewModel.Nm_usuario
                };

                var retorno = _usuarioAppService.EditarUsuario(usuario);
                ViewBag.Mensagem = retorno.mensagem.ToString();
                ViewBag.Codigo = retorno.cdretorno;

                if (ViewBag.Codigo == 0)
                {
                    //Atualiza dados editados na sessão
                    var currentUser = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cd_usuario"));
                    currentUser.NmEmail = contaViewModel.Nm_email;
                    currentUser.NmUsuario = contaViewModel.Nm_usuario;
                    currentUser.NmSenha = contaViewModel.Nm_senha;

                    HttpContext.Session.Clear();
                    HttpContext.Session.SetString("Cd_usuario", JsonConvert.SerializeObject(currentUser));

                    return RedirectToAction("Detalhar"); //colocar pop-up aqui informando a msg
                }
            }
            else
            {
                //erro
                ViewBag.Mensagem = "Alterações não estão validas!";
                ViewBag.Codigo = 1;
            }

            return View(contaViewModel);
        }


        public IActionResult Cancelar()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //passando dados da sessão para a viewModel exibir em tela        
                    var currentUser = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cd_usuario"));

                    Usuario usuario = new Usuario
                    {
                        CdUsuario = currentUser.CdUsuario,
                        NmUsuario = currentUser.NmUsuario,
                        NmEmail = currentUser.NmEmail,
                        NmSenha = currentUser.NmSenha,
                        DvAtivo = false
                    };

                    var retorno = _usuarioAppService.AlterarStatusUsuario(usuario);
                    ViewBag.Mensagem = retorno.mensagem;
                    ViewBag.Codigo = retorno.cdretorno;

                    if (ViewBag.Codigo == 0)
                    {
                        //Limpando sessão atual
                        HttpContext.Session.Clear();

                        return RedirectToAction("Entrar", "Conta");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { msg = ex.InnerException.ToString() });
                }
            }
            else
            {
                ViewBag.Cd_codigo = 1;
                ViewBag.Mensagem = "Cancelamento Invalido!";
            }

            return View();
        }


        public IActionResult Sair()
        {
            //Limpando sessão atual
            HttpContext.Session.Clear();
            return RedirectToAction("Entrar", "Conta");
        }

    }
}