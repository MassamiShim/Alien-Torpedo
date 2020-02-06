using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlienTorpedoAPI.Models;
using System.Security.Cryptography;
using System.Text;

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

        // POST api/Usuario/CadastraUsuario
        [HttpPost]
        public IActionResult CadastraUsuario([FromBody]Usuario user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.NmSenha = CriptografaSenha(user.NmSenha);

            _dbcontext.Add(user);
            _dbcontext.SaveChanges();

            return Ok("Usuário cadastrado com sucesso!");
        }

        [HttpPut]
        public IActionResult AlteraSenha([FromBody] Usuario user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null || string.IsNullOrEmpty(user.NmSenha))
            {
                return BadRequest("Favor fornecer a nova senha!");
            }

            //Selecionando usuário
            var UsuarioCadastrado = _dbcontext.Usuario.FirstOrDefault(u => u.CdUsuario == user.CdUsuario);

            UsuarioCadastrado.NmSenha = CriptografaSenha(user.NmSenha);

            _dbcontext.Usuario.Update(UsuarioCadastrado);
            _dbcontext.SaveChanges();

            return Ok("Senha alterada com sucesso!");
        }


        private string CriptografaSenha(string senha)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(inputBytes);

            // Converter byte array para string hexadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}