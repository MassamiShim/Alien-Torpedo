using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlienTorpedoAPI.Models
{
    public partial class GrupoEvento
    {
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
