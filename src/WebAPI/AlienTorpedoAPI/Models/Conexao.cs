using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace AlienTorpedoAPI.Models
{
    public class Conexao
    {
        public readonly IConfiguration _configuration;

        public Conexao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConexao()
        {
            var conexao = new SqlConnection();
            try
            {
                string connectionStrings = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
                conexao = new SqlConnection(connectionStrings);
                    
            }
            catch (Exception erro)
            {
                throw erro;
            }

            return conexao;

        }


    }
}
