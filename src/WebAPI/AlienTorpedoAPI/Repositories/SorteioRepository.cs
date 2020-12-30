using AlienTorpedoAPI.Models;
using AlienTorpedoAPI.TransferObject;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlienTorpedoAPI.Repositories
{
    public class SorteioRepository
    {
        public int IdGrupoEvento, CdGrupo, CdEvento, CdUsuario;
        public string NmDescricao;
        public DateTime DtEvento;
        private readonly IConfiguration _configuration;

        public SorteioRepository()
        {

        }

        public SorteioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GeraSorteio
        public int ExecutaSorteio(Dictionary<dynamic, dynamic> evento)
        {
            Random rand = new Random();
            int max = 0, sum = 0, target = 0;
            max = evento.Max(s => s.Value);

            foreach (var item in evento)
            {
                sum += (max - item.Value + 1);
            }

            target = rand.Next(1, sum);

            foreach (var item in evento.Select(s => new { s.Key, s.Value }))
            {
                var CodEvento = item.Key;
                var Qtd = item.Value;

                if ((max - Qtd + 1) >= target)
                {
                    return CodEvento;
                }
                else
                    target -= (max - Qtd + 1);
            }
            return 0;
        }

        public List<dynamic> ListarEventosSorteados(IConfiguration configuration)
        {
            String sommand = @"Select g.NmGrupo, NmEvento, NmEndereco, es.DtEvento
                            From Evento e(nolock)
                            join GrupoEvento ge(nolock) on
	                            ge.CdEvento = e.CdEvento
                            join EventoSorteado es(nolock) on
	                            ge.IdGrupoEvento = es.IdEventoSorteado
                            join Grupo g on 
	                            ge.CdGrupo = g.CdGrupo 
                            order by DtEvento desc ";

            Conexao conexao = new Conexao(_configuration);

            using(var conn = conexao.GetConexao())
            {
                return conn.Query<dynamic>(sommand).ToList();
            }
        }

        public int GeraSorteio(Grupo grupo, dbAlienContext dbContext, IConfiguration configuration)
        {
            int resultado = 0;
            List<int> categoriasEventos = ObtemCategoriasDeEventos(grupo.CdGrupo.Value);

            if (categoriasEventos.Count == 0)
                return 0;

            foreach (int categoria in categoriasEventos)
            {
                int eventoSorteado = 0;
                var evento = ObtemEventosParaSortear(grupo.CdGrupo.Value, categoria);
                eventoSorteado = ExecutaSorteio(evento);

                GrupoEvento item = EventoRepository.ObtemGrupoEvento(grupo.CdGrupo.Value, eventoSorteado, configuration);

                GravaSorteio(dbContext, item, eventoSorteado);
                resultado++;
            }

            return resultado;
        }

        #endregion GeraSorteio

        public void GravaSorteio(dbAlienContext dbContext, GrupoEvento vargrupoEvento, int cdEvento)
        {
            DateTime dataSorteio = DateTime.Now;
            EventoSorteado eventoSorteado = new EventoSorteado();
            eventoSorteado.IdGrupoEvento = vargrupoEvento.IdGrupoEvento;
            
            if (vargrupoEvento.DvRecorrente)
            {
                dataSorteio = BuscaDataSorteioRecorrente(vargrupoEvento.IdGrupoEvento);

                if (dataSorteio == DateTime.MinValue)
                    dataSorteio = vargrupoEvento.DtInicio == (DateTime)default ? DateTime.Now : vargrupoEvento.DtInicio;
                else if(vargrupoEvento.VlDiasRecorrencia.HasValue)
                    dataSorteio = dataSorteio.AddDays((double)vargrupoEvento.VlDiasRecorrencia);

                vargrupoEvento.VlRecorrencia--;
            }
            else
            {
                dataSorteio = vargrupoEvento.DtInicio == (DateTime)default ? DateTime.Now : vargrupoEvento.DtInicio;
            }
            eventoSorteado.DtEvento = dataSorteio;

            dbContext.EventoSorteado.Add(eventoSorteado);
            dbContext.GrupoEvento.Update(vargrupoEvento);

            int id = eventoSorteado.IdEventoSorteado;

            dbContext.SaveChanges();
        }

        private DateTime BuscaDataSorteioRecorrente(int idGrupoEvento)
        {
            DateTime dataSorteio = new DateTime();
            Conexao conexao = new Conexao(_configuration);

            using (var conn = conexao.GetConexao())
            {
                string command = @"
                                    Select Max(DtEvento) as max
                                    From EventoSorteado
                                    Where
	                                    IdGrupoEvento = @Id_grupo_evento";

                dataSorteio = Convert.ToDateTime(
                        conn.Query(command, new { @Id_grupo_evento = idGrupoEvento }).DefaultIfEmpty().Select(s => s.max).First()
                    );
            }

            return dataSorteio;
        }

        public Dictionary<dynamic, dynamic> ObtemEventosParaSortear(int CdGrupo, int CdTipoEvento)
        {
            Conexao conexao = new Conexao(_configuration);
            var evento = new Dictionary<dynamic, dynamic>();

            using (var conn = conexao.GetConexao())
            {
                string command = @"
                                    SELECT e.CdEvento, Qtd = COUNT(e.CdEvento)
                                    FROM Evento e(NOLOCk)
                                    Left JOIN GrupoEvento ge(NOLOCk) ON
	                                    ge.CdEvento = e.CdEvento
                                    Left Join EventoSorteado es(NOLOCk) ON
	                                    es.IdGrupoEvento = ge.IdGrupoEvento
                                    WHERE ge.CdGrupo = @CdGrupo and CdTipoEvento = @CdTipoEvento
                                    GROUP BY e.CdEvento ";

                evento = conn.Query(command, new { CdGrupo, CdTipoEvento }).ToDictionary(k => k.CdEvento, v => v.Qtd);
            }

            return evento;
        }
        
        private List<int> ObtemCategoriasDeEventos(int CdGrupo)
        {
            Conexao conexao = new Conexao(_configuration);
            List<int> evento = new List<int>();

            using (var conn = conexao.GetConexao())
            {
                string command = @"
                                    SELECT distinct CdTipoEvento
                                    From GrupoEvento ge
                                    join Evento e on
	                                    ge.CdEvento = e.CdEvento                                   
                                    Where
	                                    CdGrupo = @Cd_grupo ";

                evento = conn.Query<int>(command, new { @Cd_grupo = CdGrupo }).ToList();
            }

            return evento;
        }
    }
}
