using AlienTorpedoAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace AlienTorpedoAPI.Repositories
{
    public static class EventoRepository
    {        
        public static void AdicionarEvento(Evento evento, IConfiguration configuration)
        {
            try
            {

                Conexao conexao = new Conexao(configuration);

                using (var conn = conexao.GetConexao())
                {
                    string command = @"
                                    INSERT INTO Evento
                                    (
                                         CdTipoEvento
                                        ,NmEvento
                                        ,NmEndereco
                                        ,VlEvento
                                        ,VlNota
                                        ,DvParticular
                                    )
                                    VALUES
                                    (
                                         @CdTipoEvento
                                        ,@NmEvento
                                        ,@NmEndereco
                                        ,@VlEvento
                                        ,@VlNota
                                        ,@DvParticular
                                    )
                                ";

                    conn.Execute(command, evento);
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public static List<Evento> ListarEventos(IConfiguration configuration)
        {
            string command = @"SELECT * FROM Evento(NOLOCK) ";

            try
            {
                Conexao conexao = new Conexao(configuration);

                using (var conn = conexao.GetConexao())
                {
                    var eventos = conn.Query<Evento>(command).ToList();
                    return eventos;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static GrupoEvento ObtemGrupoEvento(int CdGrupo, int CdEvento, IConfiguration configuration)
        {
            try
            {
                string command = @" select * from GrupoEvento(NOLOCK) where CdGrupo = @CdGrupo and CdEvento = @CdEvento";

                Conexao conexao = new Conexao(configuration);

                using (var conn = conexao.GetConexao())
                {
                    var grupoEvento = conn.Query<GrupoEvento>(command, new { CdGrupo, CdEvento }).FirstOrDefault();
                    return grupoEvento;
                }
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public static void AtrelarEventoAGrupo(GrupoEvento evento, IConfiguration configuration)
        {
            try
            {

                Conexao conexao = new Conexao(configuration);

                using (var conn = conexao.GetConexao())
                {
                    string command = @"
                                    IF NOT EXISTS(SELECT 1 FROM GrupoEvento where CdGrupo = @CdGrupo and CdEvento = @CdEvento)
                                    BEGIN
                                        INSERT INTO GrupoEvento
                                        (
                                             CdGrupo
                                            ,CdEvento
                                            ,NmDescricao
                                            ,DtCadastro
                                            ,DtInicio
                                            ,DvRecorrente
                                            ,VlRecorrencia
                                            ,VlDiasRecorrencia
                                        )
                                        VALUES
                                        (
                                             @CdGrupo
                                            ,@CdEvento
                                            ,@NmDescricao
                                            ,@DtCadastro
                                            ,@DtInicio
                                            ,@DvRecorrente
                                            ,@VlRecorrencia
                                            ,@VlDiasRecorrencia
                                        )
                                    END                                    
                                ";

                    conn.Execute(command, evento);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

    }
}
