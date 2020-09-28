using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlienTorpedoAPI.Models
{
    public partial class GrupoEvento
    {
        public GrupoEvento()
        {

        }

        public GrupoEvento(int IdGrupoEvento, int CdGrupo, int? CdEvento, string NmDescricao, DateTime DtEvento)
        {
            this.IdGrupoEvento = IdGrupoEvento;
            this.CdGrupo = CdGrupo;
            this.CdEvento = CdEvento;
            this.NmDescricao = NmDescricao;
            this.DtEvento = DtEvento;
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int? IdGrupoEvento { get; set; }
        public int CdGrupo { get; set; }
        public int? CdEvento { get; set; }
        public string NmDescricao { get; set; }
        public DateTime? DtEvento { get; set; }

        public virtual Evento CdEventoNavigation { get; set; }
        public virtual Grupo CdGrupoNavigation { get; set; }
    }
}
