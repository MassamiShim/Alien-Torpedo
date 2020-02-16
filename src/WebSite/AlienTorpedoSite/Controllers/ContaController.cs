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
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        //Chamando API para validar o login ..

                        //Recebendo retorno da API
                        Usuario usuario = new Usuario
                        {
                            CdUsuario = 4,
                            NmUsuario = "Frank Barba",
                            NmEmail = "frank@barba.com",
                            NmSenha = "beard",
                            DvAtivo = true,
                            DtInclusao = DateTime.Parse("2017-09-19 14:58:20.083")
                        };

                        if (usuario != null)
                        {
                            //redirecionar para outra tela, se sucesso
                            if (usuario.DvAtivo == true)
                            {
                                //Armazenando dados do usuário em sessão
                                HttpContext.Session.SetString("Cd_usuario", usuario.CdUsuario.ToString());
                                HttpContext.Session.SetString("Nm_usuario", usuario.NmUsuario);
                                HttpContext.Session.SetString("Nm_email", usuario.NmEmail);
                                HttpContext.Session.SetString("Nm_senha", usuario.NmSenha);
                                HttpContext.Session.SetString("Dv_ativo", usuario.DvAtivo.ToString());
                                HttpContext.Session.SetString("Dt_inclusao", usuario.DtInclusao.ToString());

                                ViewBag.Mensagem = "Acesso liberado!";
                                ViewBag.Codigo = 0;

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Mensagem = "Conta cancelada!";
                                ViewBag.Codigo = 1;
                            }
                        }
                        else
                        {
                            ViewBag.Mensagem = "Acesso negado! Confirme se dados estão corretos.";
                            ViewBag.Codigo = 1;
                        }
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
                ViewBag.Mensagem = "Dados Invalidos!";
                ViewBag.Codigo = 1;
            }

            return View(entrarViewModel);
        }
        

        public IActionResult Detalhar()
        {
            ViewData["Title"] = "Sua Conta";
            
            //passando dados da sessão para a viewModel exibir em tela
            ContaViewModel contaViewModel = new ContaViewModel
            {
                Cd_usuario = Int32.Parse(HttpContext.Session.GetString("Cd_usuario")),
                Nm_email = HttpContext.Session.GetString("Nm_email"),
                Nm_usuario = HttpContext.Session.GetString("Nm_usuario"),
                Dv_ativo = bool.Parse(HttpContext.Session.GetString("Dv_ativo")),
                Dt_inclusao = DateTime.Parse(HttpContext.Session.GetString("Dt_inclusao"))
            };

            ViewData["Title"] = "Sua Conta";
            return View(contaViewModel);
        }


        public IActionResult Editar()
        {
            ViewData["Title"] = "Edição de Conta";
            
            //Passar dados da sessão para a ContaViewModel exibir em tela
            ContaViewModel contaViewModel = new ContaViewModel
            {
                Cd_usuario = Int32.Parse(HttpContext.Session.GetString("Cd_usuario")),
                Nm_email = HttpContext.Session.GetString("Nm_email"),
                Nm_usuario = HttpContext.Session.GetString("Nm_usuario"),
                Nm_senha = HttpContext.Session.GetString("Nm_senha"),
                Dv_ativo = bool.Parse(HttpContext.Session.GetString("Dv_ativo")),
                Dt_inclusao = DateTime.Parse(HttpContext.Session.GetString("Dt_inclusao"))
            };
            
            return View(contaViewModel);
        }

        [HttpPost]
        public IActionResult Editar(ContaViewModel contaViewModel)
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
                        client.BaseAddress = new System.Uri("http://localhost:65346/api/Usuario/EditaUsuario");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        
                        string stringData = JsonConvert.SerializeObject(usuario);
                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                        //Chamando API passando o arquivo JSON                                              
                        HttpResponseMessage response = client.PutAsync("http://localhost:65346/api/Usuario/EditaUsuario", contentData).Result;

                        //Passando retorno da API para uma string
                        string retorno = response.Content.ReadAsStringAsync().Result;
                        dynamic resultado = JsonConvert.DeserializeObject(retorno);

                        //Enviando retorno para a tela
                        ViewBag.Mensagem = resultado.mensagem.ToString();
                        ViewBag.Codigo = Int32.Parse(resultado.cdretorno.ToString());

                        if (ViewBag.Codigo == 0)
                        {
                            //Atualiza dados editados na sessão
                            HttpContext.Session.SetString("Nm_usuario", usuario.NmUsuario);
                            HttpContext.Session.SetString("Nm_email", usuario.NmEmail);
                            HttpContext.Session.SetString("Nm_senha", usuario.NmSenha);

                            return RedirectToAction("Detalhar"); //colocar pop-up aqui informando a msg
                        }
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


        public IActionResult Cancelar()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //atribuindo informações editadas no objeto usuario
                    Usuario usuario = new Usuario
                    {
                        CdUsuario = Int32.Parse(HttpContext.Session.GetString("Cd_usuario")),
                        NmUsuario = HttpContext.Session.GetString("Nm_usuario"),
                        NmEmail = HttpContext.Session.GetString("Nm_email"),
                        NmSenha = HttpContext.Session.GetString("Nm_senha"),
                        DvAtivo = false
                    };

                    //Chamando API para cancelar usuário
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new System.Uri("http://localhost:65346/api/Usuario/AlteraStatus");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        string stringData = JsonConvert.SerializeObject(usuario);
                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                        //Chamando API passando o arquivo JSON                                              
                        HttpResponseMessage response = client.PutAsync("http://localhost:65346/api/Usuario/AlteraStatus", contentData).Result;

                        //Passando retorno da API para uma string
                        string retorno = response.Content.ReadAsStringAsync().Result;
                        dynamic resultado = JsonConvert.DeserializeObject(retorno);

                        //Enviando retorno para a tela
                        ViewBag.Mensagem = resultado.mensagem.ToString();
                        ViewBag.Codigo = Int32.Parse(resultado.cdretorno.ToString());

                        if (ViewBag.Codigo == 0)
                        {
                            //Limpando sessão atual
                            HttpContext.Session.Clear();

                            ViewBag.Mensagem = "Conta cancelada com sucesso!";
                            return RedirectToAction("Entrar", "Conta");
                        }
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
