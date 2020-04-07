using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlienTorpedoSite.Models.Evento
{
    public class Evento
    {
        public int? CdEvento { get; set; }
        public short? CdTipoEvento { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string NmEvento { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string NmEndereco { get; set; }
        public double? VlEvento { get; set; }
        public double? VlNota { get; set; }
        public bool? DvParticular { get; set; }
        public int? CdUsuario { get; set; }
    }
}
