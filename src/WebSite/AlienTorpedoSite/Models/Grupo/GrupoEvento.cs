using System;
using System.ComponentModel.DataAnnotations;

namespace AlienTorpedoSite.Models.Grupo
{
    public class GrupoEvento
    {
        public int? IdGrupoEvento { get; set; }

        public int CdGrupo { get; set; }

        public int CdEvento { get; set; }

        [StringLength(80)]
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        public string NmEvento { get; set; }

        [Display(Name = "Data Evento")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        public DateTime? DtEvento { get; set; }
        public string NmEndereco { get; set; }
        public double? VlEvento { get; set; }
    }
}
