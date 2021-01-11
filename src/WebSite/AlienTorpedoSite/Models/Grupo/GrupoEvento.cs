using System;
using System.ComponentModel.DataAnnotations;

namespace AlienTorpedoSite.Models.Grupo
{
    public class GrupoEvento
    {
        public int? IdGrupoEvento { get; set; }

        public int CdGrupo { get; set; }

        public int CdEvento { get; set; }

        [Display(Name = "Descrição")]       
        public string NmEvento { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O campo [{0}] deve ter no mínimo 10 e no máximo 80 caractéres!")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        public string NmDescricao { get; set; }

        [Display(Name = "Data Evento")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        public DateTime? DtEvento { get; set; }
        public string NmEndereco { get; set; }
        public double? VlEvento { get; set; }
        public string NmGrupo { get; set; }
    }
}
