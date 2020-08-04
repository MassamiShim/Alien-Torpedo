using System;
using System.ComponentModel.DataAnnotations;
using AlienTorpedoSite.Models.Evento;

namespace AlienTorpedoSite.Models.Conta
{
    public class Usuario
    {
        [Key]
        [Required]
        public int CdUsuario { get; set; }

        [Required]
        [StringLength(80)]
        public string NmEmail { get; set; }

        [Required]
        [StringLength(80)]
        public string NmUsuario { get; set; }
        
        [Required]
        [StringLength(20)]
        public string NmSenha { get; set; }

        public bool DvAtivo  { get; set; }

        [Required]
        public DateTime DtInclusao { get; set; }
    }
}
