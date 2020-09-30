using AlienTorpedoAPI.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlienTorpedoAPI.Classes
{
    public class Sorteio
    {
        public int IdGrupoEvento, CdGrupo, CdEvento, CdUsuario;
        public string NmDescricao;
        public DateTime DtEvento;
        private readonly IConfiguration _configuration;

        public Sorteio()
        {

        }

        public Sorteio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        #region GeraSorteio

        public int GeraSorteio(dbAlienContext dbContext)
        {
            int resultado = 0;
            List<GrupoEvento> grupoEventos = ObtemGrupoEventos();
            if (grupoEventos.Count == 0)
                return 0;
            foreach(GrupoEvento item in grupoEventos)
            {
                int eventoSorteado = 0;
                var evento = ObtemEventosParaSortear(item.CdGrupo, item.IdGrupoEvento);
                eventoSorteado = ExecutaSorteio(evento);
                GravaSorteio(dbContext, item, eventoSorteado);
                resultado++;
            }

            return resultado;
        }

        //public int GeraSorteio(GrupoEvento grupoEvento, dbAlienContext dbContext)
        //{
        //    int idGrupoEvento = 0, eventoSorteado = 0;

        //    idGrupoEvento = grupoEvento.IdGrupoEvento.Value;

        //    var varGrupoEvento = dbContext.GrupoEvento.FirstOrDefault(u => u.IdGrupoEvento == idGrupoEvento);
        //    var evento = ObtemEventosParaSortear(varGrupoEvento.CdGrupo, idGrupoEvento);

        //    eventoSorteado = ExecutaSorteio(evento);
        //    GravaSorteio(dbContext, varGrupoEvento, eventoSorteado);

        //    return 0;
        //}

        public int GeraSorteio(Grupo grupo, List<GrupoEvento> grupoEventos, dbAlienContext dbContext)
        {
            int eventoSorteado = 0, resultado = 0;
            var evento = ObtemEventosParaSortear(grupo.CdGrupo.Value);

            foreach (GrupoEvento item in grupoEventos)
            {
                eventoSorteado = ExecutaSorteio(evento);
                GravaSorteio(dbContext, item, eventoSorteado);
                evento[eventoSorteado] += 1;
                resultado++;
            }

            return resultado;
        }
        #endregion GeraSorteio

        internal int GeraSorteioTodos(Grupo grupo, dbAlienContext dbcontext)
        {
            List<GrupoEvento> grupoEventos = ObtemGrupoEventos(grupo.CdGrupo.Value);

            if (grupoEventos.Count == 0)
                return 0;

            int resultado = GeraSorteio(grupo, grupoEventos, dbcontext);

            return resultado;
        }

        //public void GravaSorteio(dbAlienContext dbContext, GrupoEvento vargrupoEvento, int cdEvento)
        //{
        //    vargrupoEvento.CdEvento = cdEvento;
        //    dbContext.GrupoEvento.Update(vargrupoEvento);
        //    dbContext.SaveChanges();
        //}

        public void GravaSorteio(dbAlienContext dbContext, GrupoEvento vargrupoEvento, int cdEvento)
        {
            DateTime dataSorteio = DateTime.MinValue;
            EventoSorteado eventoSorteado = new EventoSorteado();
            eventoSorteado.IdGrupoEvento = vargrupoEvento.IdGrupoEvento;
            eventoSorteado.CdEvento = cdEvento;

            if(vargrupoEvento.DvRecorrente)
            {
                dataSorteio = BuscaDataSorteioRecorrente(vargrupoEvento.IdGrupoEvento);

                if (dataSorteio == DateTime.MinValue)
                    dataSorteio = vargrupoEvento.DtInicio;
                else
                dataSorteio = dataSorteio.AddDays((double)vargrupoEvento.VlDiasRecorrencia);

                vargrupoEvento.VlRecorrencia--;
            }
            else
            {
                dataSorteio = vargrupoEvento.DtInicio;
            }
            eventoSorteado.DtEvento = dataSorteio;

            dbContext.EventoSorteado.Add(eventoSorteado);
            dbContext.GrupoEvento.Update(vargrupoEvento);
            dbContext.SaveChanges();
        }

        private DateTime BuscaDataSorteioRecorrente(int idGrupoEvento)
        {
            DateTime dataSorteio = new DateTime();
            Conexao conexao = new Conexao(_configuration);

            using (var conn = conexao.GetConexao())
            {
                string command = @"
                                    Select Max(Dt_evento) as max
                                    From Evento_sorteado
                                    Where
	                                    Id_grupo_evento = @Id_grupo_evento";

                dataSorteio = Convert.ToDateTime(
                        conn.Query(command, new { @Id_grupo_evento = idGrupoEvento }).DefaultIfEmpty().Select(s => s.max).First()
                    );
            }

            return dataSorteio;
        }

        //public List<GrupoEventoViewModel> BuscaSorteio(dbAlienContext dbContext, GrupoEvento grupoEvento)
        //{
        //    int cdEvento = 0;
        //    cdEvento = (int)grupoEvento.CdEvento;

        //    var resultado = (from ge in dbContext.GrupoEvento.Where(w => w.IdGrupoEvento == grupoEvento.IdGrupoEvento)
        //                     join e in dbContext.Evento on
        //                         new { ge.CdEvento } equals new { e.CdEvento }
        //                     select new GrupoEventoViewModel()
        //                     {
        //                         IdGrupoEvento = ge.IdGrupoEvento,
        //                         CdGrupo = ge.CdGrupo,
        //                         DtEvento = ge.DtEvento,
        //                         NmEvento = e.NmEvento,
        //                         NmEndereco = e.NmEndereco,
        //                         VlEvento = e.VlEvento
        //                     }).ToList();

        //    return resultado;
        //}

        //public Dictionary<dynamic, dynamic> ObtemEventosParaSortear(int CdGrupo, int idGrupoEvento)
        //{
        //    Conexao conexao = new Conexao(_configuration);
        //    var evento = new Dictionary<dynamic, dynamic>();

        //    using (var conn = conexao.GetConexao())
        //    {
        //        string command = @"
        //                            SELECT e.Cd_evento, Qtd = COUNT(ge.Cd_evento)
        //                            FROM Evento e
        //                            LEFT JOIN Grupo_evento ge ON
        //                             ge.Cd_evento = e.Cd_evento
        //                            AND ge.Cd_grupo = @Cd_grupo
        //                            AND ge.Id_grupo_evento != @idGrupoEvento
        //                            GROUP BY e.Cd_evento ";

        //        evento = conn.Query(command, new { @Cd_grupo = CdGrupo, idGrupoEvento }).ToDictionary(k => k.Cd_evento, v => v.Qtd);
        //    }

        //    return evento;
        //}

        public Dictionary<dynamic, dynamic> ObtemEventosParaSortear(int CdGrupo, int idGrupoEvento)
        {
            Conexao conexao = new Conexao(_configuration);
            var evento = new Dictionary<dynamic, dynamic>();

            using (var conn = conexao.GetConexao())
            {
                string command = @"
                                    SELECT e.Cd_evento, Qtd = COUNT(es.Cd_evento)
                                    FROM Evento e
                                    Left Join Evento_sorteado es On
	                                    es.Cd_evento = e.Cd_evento
                                    Left JOIN Grupo_evento ge ON
	                                    ge.Cd_grupo = @Cd_grupo
                                    and ge.Id_grupo_evento = es.Id_grupo_evento
                                    GROUP BY e.Cd_evento ";

                evento = conn.Query(command, new { @Cd_grupo = CdGrupo, idGrupoEvento }).ToDictionary(k => k.Cd_evento, v => v.Qtd);
            }

            return evento;
        }

        public Dictionary<dynamic, dynamic> ObtemEventosParaSortear(int CdGrupo)
        {
            Conexao conexao = new Conexao(_configuration);
            var evento = new Dictionary<dynamic, dynamic>();

            using (var conn = conexao.GetConexao())
            {
                string command = @"
                                    SELECT e.Cd_evento, Qtd = COUNT(ge.Cd_evento)
                                    FROM Evento e
                                    LEFT JOIN Grupo_evento ge ON
	                                    ge.Cd_evento = e.Cd_evento
                                    AND ge.Cd_grupo = @Cd_grupo
                                    AND ge.Cd_evento is not null
                                    GROUP BY e.Cd_evento ";

                evento = conn.Query(command, new { @Cd_grupo = CdGrupo }).ToDictionary(k => k.Cd_evento, v => v.Qtd);
            }

            return evento;
        }

        private List<GrupoEvento> ObtemGrupoEventos(int CdGrupo)
        {
            Conexao conexao = new Conexao(_configuration);
            List<GrupoEvento> evento = new List<GrupoEvento>();

            using (var conn = conexao.GetConexao())
            {
                string command = @"
                                    Select Id_grupo_evento,Cd_grupo,Nm_descricao
                                    From Grupo_evento
                                    Where
	                                    Cd_grupo = @Cd_grupo
                                    and Cd_evento is null";

                evento = conn.Query(command, new { @Cd_grupo = CdGrupo }).Select(s => new GrupoEvento(s.Id_grupo_evento, s.Cd_grupo, s.Nm_descricao)).ToList();
            }

            return evento;
        }
        private List<GrupoEvento> ObtemGrupoEventos()
        {
            Conexao conexao = new Conexao(_configuration);
            List<GrupoEvento> evento = new List<GrupoEvento>();

            using (var conn = conexao.GetConexao())
            {
                string command = @"
                                    Select Distinct ge.Id_grupo_evento, ge.Cd_grupo, ge.Nm_descricao, ge.Dt_cadastro, ge.Dt_inicio, ge.Dv_recorrente, ge.Vl_recorrencia, ge.Vl_dias_recorrencia 
                                    from Grupo_evento ge
                                    Left Join Evento_sorteado es On
	                                    es.Id_grupo_evento = ge.Id_grupo_evento
                                    Where
                                        ((DATEDIFF(d, ge.Dt_inicio, getdate()) = 0
		                                    and ge.Dv_recorrente = 0
                                            and es.Cd_evento is null)
	                                    or
	                                    (ge.Dv_recorrente = 1
		                                    and ge.Vl_recorrencia > 0))";

                evento = conn.Query(command, new { @Cd_grupo = CdGrupo }).Select(s => new GrupoEvento(s.Id_grupo_evento, s.Cd_grupo, s.Nm_descricao, s.Dt_cadastro, s.Dt_inicio, s.Dv_recorrente, s.Vl_recorrencia, s.Vl_dias_recorrencia)).ToList();
            }

            return evento;
        }
    }
}
