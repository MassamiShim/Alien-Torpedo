using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlienTorpedoAPI.Models
{
    public partial class EventoSorteado
    {
        public EventoSorteado()
        {

        }

        public EventoSorteado(int IdGrupoEvento, int? CdEvento, DateTime DtEvento)
        {
            this.IdGrupoEvento = IdGrupoEvento;
            this.CdEvento = CdEvento;
            this.DtEvento = DtEvento;
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdEventoSorteado { get; set; }
        public int IdGrupoEvento { get; set; }
        public int? CdEvento { get; set; }
        public DateTime DtEvento { get; set; }
    }
}
