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
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        public string NmTipoEvento { get; set; }
    }
}
