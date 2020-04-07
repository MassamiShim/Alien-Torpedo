using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlienTorpedoSite.Models.Grupo
{
    public class Grupo
    {
        public int? CdGrupo { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string NmGrupo { get; set; }
       
        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public DateTime DtInclusao { get; set; }
    }
}
