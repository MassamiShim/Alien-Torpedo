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
                                         Cd_tipo_evento
                                        ,Nm_evento
                                        ,Nm_endereco
                                        ,Vl_evento
                                        ,Vl_nota
                                        ,Dv_particular
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
            string command = @"
                                SELECT
	                                 CdEvento =Cd_evento
	                                ,CdTipoEvento = Cd_tipo_evento
	                                ,NmEvento = Nm_evento
	                                ,NmEndereco = Nm_endereco
	                                ,VlEvento = Vl_evento
	                                ,VlNota = Vl_nota
	                                ,DvParticular = Dv_particular
                                FROM Evento(NOLOCK) ";

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

        public static void AtrelarEventoAGrupo(GrupoEvento evento, IConfiguration configuration)
        {
            try
            {

                Conexao conexao = new Conexao(configuration);

                using (var conn = conexao.GetConexao())
                {
                    string command = @"
                                    IF NOT EXISTS(SELECT 1 FROM Grupo_evento where Cd_grupo = @CdGrupo and Cd_evento = @CdEvento)
                                    BEGIN
                                        INSERT INTO Grupo_evento
                                        (
                                             Cd_grupo
                                            ,Cd_evento
                                            ,Nm_descricao
                                            ,Dt_cadastro
                                            ,Dt_inicio
                                            ,Dv_recorrente
                                            ,Vl_recorrencia
                                            ,Vl_dias_recorrencia
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
