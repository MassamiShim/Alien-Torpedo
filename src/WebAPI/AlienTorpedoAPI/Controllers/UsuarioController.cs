using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlienTorpedoAPI.Models;
using AlienTorpedoAPI.Classes;
using Newtonsoft.Json;

namespace AlienTorpedoAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class UsuarioController : Controller
    {
        //Vinicius - Construtor para iniciar dbcontext - INI
        private readonly dbAlienContext _dbcontext;

        public UsuarioController(dbAlienContext dbContext)
        {
            _dbcontext = dbContext;
        }
        //Vinicius - Construtor para iniciar dbcontext - FIM

        // GET api/Usuario/AutenticarUsuario
        [HttpGet]
        public IActionResult AutenticarUsuario(string NmEmail, string NmSenha)
        {
            NmSenha = Senha.CriptografaSenha(NmSenha);

            var user = _dbcontext.Usuario.FirstOrDefault(x => x.NmEmail == NmEmail && x.NmSenha == NmSenha);

            if(user != null)
            {
                if (!user.DvAtivo.Value)
                {
                    return Json(new { cdretorno = 1, mensagem = "Conta cancelada!" });
                }

                return Json(new { cdretorno = 0, mensagem = "Usuário autenticado com sucesso!", usuario = new Usuario { CdUsuario = user.CdUsuario, NmUsuario = user.NmUsuario, NmEmail = user.NmEmail, NmSenha = user.NmSenha, DvAtivo = user.DvAtivo, DtInclusao = user.DtInclusao } });
            }
            else
            {
                return Json(new { cdretorno = 1, mensagem = "Usuário ou senha inválido." });
            }            
        }

        // POST api/Usuario/CadastraUsuario
        [HttpPost]
        public IActionResult CadastraUsuario([FromBody]Usuario user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { cdretorno = 1, mensagem = "Chamada fora do padrão, favor verificar!" });
            }

            try
            {
                user.CdUsuario = null;
                user.NmSenha = Senha.CriptografaSenha(user.NmSenha.ToString());
                user.DtInclusao = DateTime.Now;

                _dbcontext.Add(user);
                _dbcontext.SaveChanges();

                return Json(new { cdretorno = 0, mensagem = "Usuário cadastrado com sucesso" });
            }
            catch(Exception ex)
            {
                return Json(new { cdretorno = 1, mensagem = String.Format("Erro ao cadastrar usuário. Para mais informações consulte: {0}", ex.ToString())});
            }

        }

        // PUT api/Usuario/AlteraSenha
        [HttpPut]
        public IActionResult AlteraSenha([FromBody] Usuario user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { cdretorno = 1, mensagem = "Chamada fora do padrão, favor verificar!" });
            }

            if (user == null || string.IsNullOrEmpty(user.NmSenha))
            {
                return Json(new { cdretorno = 1, mensagem = "Favor fornecer a nova senha!" });
            }

            var CdRetorno = Senha.AlteraSenha(user.CdUsuario.Value, user.NmSenha, _dbcontext);

            if (CdRetorno == 0)
            {
                return Json(new { cdretorno = 0, mensagem = "Senha alterada com sucesso!" });
            }
            else
            {
                return Json(new { cdretorno = 1, mensagem = "Falha ao alterar senha, favor verificar!" });
            }
            
        }
        // PUT api/Usuario/EditaUsuario
        [HttpPut]
        public IActionResult EditaUsuario([FromBody] Usuario user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { cdretorno = 1, mensagem = "Chamada fora do padrão, favor verificar!" });
            }

            try
            {
                var UsuarioCadastrado = _dbcontext.Usuario.FirstOrDefault(u => u.CdUsuario == user.CdUsuario);

                UsuarioCadastrado.NmUsuario = user.NmUsuario;
                UsuarioCadastrado.NmEmail = user.NmEmail;
                if (UsuarioCadastrado.NmSenha != user.NmSenha)
                {
                    UsuarioCadastrado.NmSenha = Senha.CriptografaSenha(user.NmSenha);
                }
                _dbcontext.Usuario.Update(UsuarioCadastrado);
                _dbcontext.SaveChanges();

                return Json(new { cdretorno = 0, mensagem = "Usuário alterado com sucesso!" });
            }
            catch
            {
                return Json(new { cdretorno = 1, mensagem = "Falha ao alterar usuário, favor verificar!" });
            }
        }

        // PUT api/Usuario/AlteraStatus
        [HttpPut]
        public IActionResult AlteraStatus([FromBody] Usuario user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { cdretorno = 1, mensagem = "Chamada fora do padrão, favor verificar!" });
            }

            try
            {
                var UsuarioCadastrado = _dbcontext.Usuario.FirstOrDefault(u => u.CdUsuario == user.CdUsuario);

                UsuarioCadastrado.DvAtivo = user.DvAtivo;
                _dbcontext.Usuario.Update(UsuarioCadastrado);
                _dbcontext.SaveChanges();

                return Json(new { cdretorno = 0, mensagem = "Status alterado com sucesso!" });
            }
            catch
            {
                return Json(new { cdretorno = 1, mensagem = "Falha ao alterar status, favor verificar!" });
            }
        }
    }
}