using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlienTorpedoAPI.Models
{
    public partial class EventoSorteado
    {
        public EventoSorteado()
        {

        }

        public EventoSorteado(int IdGrupoEvento, DateTime DtEvento)
        {
            this.IdGrupoEvento = IdGrupoEvento;
            this.DtEvento = DtEvento;
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdEventoSorteado { get; set; }
        public int IdGrupoEvento { get; set; }
        public DateTime DtEvento { get; set; }
    }
}
