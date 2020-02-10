using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlienTorpedoAPI.Models
{
    public partial class TipoEvento
    {
        public TipoEvento()
        {
            Evento = new HashSet<Evento>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public short? CdTipoEvento { get; set; }
        public string NmTipoEvento { get; set; }

        public virtual ICollection<Evento> Evento { get; set; }
    }
}
