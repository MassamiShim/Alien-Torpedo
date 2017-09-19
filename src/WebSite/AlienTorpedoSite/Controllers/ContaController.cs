using Microsoft.AspNetCore.Mvc;
using AlienTorpedoSite.ViewModels.Conta;
using AlienTorpedoSite.Models.Conta;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

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
                try
                {
                    //atribuindo informações cadastradas ao objeto usuario
                    Usuario usuario = new Usuario
                    {
                        NmEmail = contaViewModel.Nm_email,
                        NmSenha = contaViewModel.Nm_senha,
                        NmUsuario = contaViewModel.Nm_usuario,
                        DvAtivo = true,
                        DtInclusao = DateTime.UtcNow //formato padrão
                    };

                    //Chamando API para cadastrar o usuário na base 
                    using (HttpClient client = new HttpClient())
                    {
                        //Setando endereço da API
                        client.BaseAddress = new System.Uri("http://localhost:65338/api/Usuario/CadastraUsuario");
                        //Limpando header
                        client.DefaultRequestHeaders.Accept.Clear();
                        //Adicionando um novo header do tipo JSON
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        //Transformando obj Usuario em uma string
                        string stringData = JsonConvert.SerializeObject(usuario);

                        //Transformando string em um arquivo do tipo JSON
                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                        //Chamando API passando o arquivo JSON
                        HttpResponseMessage response = client.PostAsync("http://localhost:65338/api/Usuario/CadastraUsuario", contentData).Result;

                        //Passando retorno da API para uma string
                        string retorno = response.Content.ReadAsStringAsync().Result;

                        //Fragmentando retorno do arquivo json 
                        dynamic resultado = JsonConvert.DeserializeObject(retorno);

                        //Enviando retorno para a tela
                        ViewBag.Mensagem = resultado.mensagem.ToString();
                        ViewBag.Codigo = Int32.Parse(resultado.cdretorno.ToString());
                        
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { msg = ex.InnerException.ToString() });
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
                //chamar API aqui para validar o login e receber dados do usuário

                Usuario usuario = new Usuario { DvAtivo = true };//retorno da api

                //armazenando dados do usuário em sessão
                HttpContext.Session.SetString("Cd_usuario", "4");
                HttpContext.Session.SetString("Nm_usuario", "Frank Wesley");
                HttpContext.Session.SetString("Nm_email", "frank@gmail.com");
                HttpContext.Session.SetString("Nm_senha", "123");
                HttpContext.Session.SetString("Dv_ativo", "true");
                HttpContext.Session.SetString("Dt_inclusao", DateTime.UtcNow.ToString());

                //redirecionar para outra tela, se sucesso
                if (usuario.DvAtivo == true)
                {
                    ViewBag.Mensagem = "Acesso liberado!";
                    ViewBag.Codigo = 0;

                    return RedirectToAction("Index", "Home");
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
                Cd_usuario = Int32.Parse(HttpContext.Session.GetString("Cd_usuario")),
                Nm_email = HttpContext.Session.GetString("Nm_email"),
                Nm_usuario = HttpContext.Session.GetString("Nm_usuario"),
                Dv_ativo = bool.Parse(HttpContext.Session.GetString("Dv_ativo")),
                Dt_inclusao = DateTime.Parse(HttpContext.Session.GetString("Dt_inclusao"))
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
                Cd_usuario = 4,
                Nm_email = "frank@gmail.com",
                Nm_usuario = "frank",
                Nm_senha = "wesley",
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
                try
                {
                    //atribuindo informações editadas no objeto usuario
                    Usuario usuario = new Usuario
                    {
                        CdUsuario = contaViewModel.Cd_usuario,
                        NmEmail = contaViewModel.Nm_email,
                        NmSenha = contaViewModel.Nm_senha,
                        NmUsuario = contaViewModel.Nm_usuario,
                        DvAtivo = contaViewModel.Dv_ativo
                    };

                    //Chamando API para aplicar as alterações no usuário
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new System.Uri("http://localhost:65338/api/Usuario/EditaUsuario");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        
                        string stringData = JsonConvert.SerializeObject(usuario);
                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                        //Chamando API passando o arquivo JSON                                              
                        HttpResponseMessage response = client.PutAsync("http://localhost:65338/api/Usuario/EditaUsuario", contentData).Result;

                        //Passando retorno da API para uma string
                        string retorno = response.Content.ReadAsStringAsync().Result;
                        dynamic resultado = JsonConvert.DeserializeObject(retorno);

                        //Enviando retorno para a tela
                        ViewBag.Mensagem = resultado.mensagem.ToString();
                        ViewBag.Codigo = Int32.Parse(resultado.cdretorno.ToString());

                        if (ViewBag.Codigo == 0)
                            return RedirectToAction("Detalhar"); //colocar pop-up aqui informando a msg

                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { msg = ex.InnerException.ToString() });
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


        public IActionResult Cancelar(int Cd_usuario)
        {
            //Chamar API para cancelar o usuário e passar parametros necessários

            bool retorno_API = true;
            //verificar retorno da API

            if (retorno_API)
            {
                //Limpando sessão atual
                HttpContext.Session.Clear();

                ViewBag.Mensagem = "Conta cancelada com sucesso!";
                return RedirectToAction("Entrar", "Conta");
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
            //Limpando sessão atual
            HttpContext.Session.Clear();
            return RedirectToAction("Entrar", "Conta");
        }

    }
}
