using AlienTorpedoAPI.Models;
using AlienTorpedoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AlienTorpedoAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class UsuarioController : Controller
    {        
        private readonly dbAlienContext _dbcontext;

        public UsuarioController(dbAlienContext dbContext)
        {
            _dbcontext = dbContext;
        }
     
        // GET api/Usuario/AutenticarUsuario
        [HttpGet]
        public IActionResult AutenticarUsuario(string NmEmail, string NmSenha)
        {
            NmSenha = SenhaRepository.CriptografaSenha(NmSenha);

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
                 bool emailCadastrado = _dbcontext.Usuario.Count(x => x.NmEmail == user.NmEmail) > 0;

                if (emailCadastrado)
                {
                    return Json(new { cdretorno = 1, mensagem = "Este e-mail já está sendo utilizado por outro usuário, favor verificar!" });
                }

                user.CdUsuario = null;
                user.NmSenha = SenhaRepository.CriptografaSenha(user.NmSenha.ToString());
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

            var CdRetorno = SenhaRepository.AlteraSenha(user.CdUsuario.Value, user.NmSenha, _dbcontext);

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
                    UsuarioCadastrado.NmSenha = SenhaRepository.CriptografaSenha(user.NmSenha);
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