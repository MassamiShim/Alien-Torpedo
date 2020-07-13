using AlienTorpedoAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AlienTorpedoAPI.Classes
{
    public class Sorteio
    {
        public int IdGrupoEvento, CdGrupo, CdEvento, CdUsuario;
        public string NmDescricao;
        public DateTime DtEvento;

        public Sorteio()
        {
        }

        public void GeraSorteio(int idGrupoEvento, dbAlienContext dbContext)
        {
            var GrupoEvento = dbContext.GrupoEvento.FirstOrDefault(u => u.IdGrupoEvento == idGrupoEvento);
            GrupoEvento.CdEvento = 1;

            dbContext.GrupoEvento.Update(GrupoEvento);
            dbContext.SaveChanges();
        }

        public int GeraSorteio(GrupoEvento grupoEvento, dbAlienContext dbContext)
        {
            int idGrupoEvento = 0;

            idGrupoEvento = grupoEvento.IdGrupoEvento.Value;

            var varGrupoEvento = dbContext.GrupoEvento.FirstOrDefault(u => u.IdGrupoEvento == idGrupoEvento);
            var evento = new Dictionary<dynamic, dynamic>();

            using (var conn = new SqlConnection("Server=DESKTOP-K0JDSQK\\SQLEXPRESS; Database=dbAlien; Integrated Security=SSPI;"))
            {
                string command = @"
                                    SELECT e.Cd_evento, Qtd = COUNT(*)
                                    FROM Evento e
                                    INNER JOIN Grupo_evento ge ON
	                                    ge.Cd_evento = e.Cd_evento
                                    AND ge.Cd_grupo = @Cd_grupo
                                    GROUP BY e.Cd_evento ";

                evento = conn.Query(command, new { @Cd_grupo = varGrupoEvento.CdGrupo}).ToDictionary(k => k.Cd_evento, v => v.Qtd);                
            }

            //Na forma atual, o comando ira realizar um select sem where na base, para depois aplicar o filtro

            //var evento = dbContext.Evento
            //.GroupJoin(dbContext.GrupoEvento,
            //     e => e.CdEvento,
            //    ge => ge.CdEvento,
            //    (e, ge) => new
            //    {
            //        e,
            //        grupo = ge.Where(w => w.CdGrupo == varGrupoEvento.CdGrupo)
            //    })
            //.GroupBy(g => new
            //{
            //    CodEvento = g.e.CdEvento,
            //    Qtd = g.grupo.Count()
            //})
            //.Select(s => new
            //{
            //    s.Key.CodEvento,
            //    s.Key.Qtd
            //})
            //.DefaultIfEmpty()
            //.ToList();

            Random rand = new Random();
            int max = 0, count = 0, sum = 0, target = 0;
            count = evento.Count();
            sum = evento.Sum(s => s.Value);
            max = count * 10 - sum;
            target = rand.Next(1, max);

            foreach (var item in evento.Select(s => new { s.Key, s.Value }))
            {
                var CodEvento = item.Key;
                var Qtd = item.Value;

                if ((10 - Qtd) >= target)
                {
                    grupoEvento.CdEvento = CodEvento;
                    GravaSorteio(dbContext, varGrupoEvento, CodEvento);
                    return CodEvento;
                }
                else
                    target -= (10 - Qtd);
            }
            return 0;
        }

        public void GravaSorteio(dbAlienContext dbContext, GrupoEvento vargrupoEvento, int cdEvento)
        {
            vargrupoEvento.CdEvento = cdEvento;
            dbContext.GrupoEvento.Update(vargrupoEvento);
            dbContext.SaveChanges();
        }

        public string BuscaSorteio(dbAlienContext dbContext, GrupoEvento grupoEvento)
        {
            int cdEvento = 0;
            cdEvento = (int)grupoEvento.CdEvento;

            var dado = new
            {
                resultado =
                dbContext.GrupoEvento.Where(w => w.IdGrupoEvento == grupoEvento.IdGrupoEvento)
                .Join(dbContext.Evento,
                      ge => ge.CdEvento,
                      e => e.CdEvento,
                      (ge, e) => new { ge, e }
                )
                .Select(s => new
                {
                    s.ge.IdGrupoEvento,
                    s.ge.CdGrupo,
                    s.ge.DtEvento,
                    s.e.NmEvento,
                    s.e.NmEndereco,
                    s.e.VlEvento
                })
            };
            return JsonConvert.SerializeObject(dado);
        }
    }
}
