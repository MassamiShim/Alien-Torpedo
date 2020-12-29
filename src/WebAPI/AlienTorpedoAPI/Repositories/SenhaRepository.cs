using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlienTorpedoAPI;
using AlienTorpedoAPI.Models;

namespace AlienTorpedoAPI.Repositories
{
    public static class SenhaRepository
    {
        public static int AlteraSenha(int CdUsuario, string NovaSenha, dbAlienContext dbcontext)
        {
            //Selecionando usuário
            try
            {
                var UsuarioCadastrado = dbcontext.Usuario.FirstOrDefault(u => u.CdUsuario == CdUsuario);

                UsuarioCadastrado.NmSenha = CriptografaSenha(NovaSenha);

                dbcontext.Usuario.Update(UsuarioCadastrado);
                dbcontext.SaveChanges();
                return 0;
            }

            catch
            {
                return 1;
            }
        }

        public static string CriptografaSenha(string senha)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
                byte[] hash = md5.ComputeHash(inputBytes);
                
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return sb.ToString();
        }

    }
}
