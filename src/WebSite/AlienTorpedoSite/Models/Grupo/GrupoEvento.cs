using System;
using System.ComponentModel.DataAnnotations;

namespace AlienTorpedoSite.Models.Grupo
{
    public class GrupoEvento
    {
        [Key]
        [Required]
        public int IdGrupoEvento { get; set; }

        public int CdGrupo { get; set; }

        public int CdEvento { get; set; }

        [StringLength(80)]
        public string NmDescricao { get; set; }

        [Required]
        public DateTime DtEvento { get; set; }
        
    }
}
