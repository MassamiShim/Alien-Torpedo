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
        [Display(Name = "Grupo")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "O nome do [{0}] deve ter no mínimo 5 e no máximo 60 caractéres!")]
        public string NmGrupo { get; set; }
       
        [Display(Name = "Data Inclusão")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        public DateTime DtInclusao { get; set; }
    }
}
