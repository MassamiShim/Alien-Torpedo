using System;
using System.ComponentModel.DataAnnotations;

namespace AlienTorpedoSite.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Cd_usuario { get; set; }

        [Required]
        [StringLength(80)]
        public string Nm_email { get; set; }

        [Required]
        [StringLength(80)]
        public string Nm_usuario { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Nm_senha { get; set; }

        public bool Dv_ativo  { get; set; }

        [Required]
        public DateTime Dt_inclusao { get; set; }
        
    }
}
