using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlienTorpedoAPI.Models
{
    public partial class Evento
    {
        public Evento()
        {
            GrupoEvento = new HashSet<GrupoEvento>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int? CdEvento { get; set; }
        public short? CdTipoEvento { get; set; }
        public string NmEvento { get; set; }
        public string NmEndereco { get; set; }
        public double? VlEvento { get; set; }
        public double? VlNota { get; set; }
        public bool? DvParticular { get; set; }

        public virtual ICollection<GrupoEvento> GrupoEvento { get; set; }
        public virtual TipoEvento CdTipoEventoNavigation { get; set; }
    }
}
