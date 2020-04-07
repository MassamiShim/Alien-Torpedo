using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlienTorpedoSite.Models
{
    public class TipoEvento
    {
        public short? CdTipoEvento { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string NmTipoEvento { get; set; }
    }
}
