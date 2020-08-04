using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlienTorpedoAPI.Models
{
    public class GrupoEventoViewModel
    {
        public int? IdGrupoEvento { get; set; }
        public int CdGrupo { get; set; }
        public DateTime? DtEvento { get; set; }
        public string NmEvento { get; set; }
        public string NmEndereco { get; set; }
        public double? VlEvento { get; set; }
    }
}
