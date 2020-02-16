using System;
using System.ComponentModel.DataAnnotations;

namespace AlienTorpedoSite.ViewModels.Conta
{
    public class ContaViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public int Cd_usuario { get; set; }
        
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail invalido!")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        [StringLength(80, ErrorMessage = "O [{0}] deve conter no máximo 80 caracteres!")]
        public string Nm_email { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        [StringLength(80, ErrorMessage = "A campo [{0}] deve conter no máximo 80 caractéres!")]
        public string Nm_usuario { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        [StringLength(20, ErrorMessage = "A campo [{0}] deve conter no máximo 20 caractéres!")]
        public string Nm_senha { get; set; }

        [Display(Name = "Status")]
        public bool Dv_ativo { get; set; }
        
        [Display(Name = "Data de Inclusão")]
        public DateTime Dt_inclusao { get; set; }
        
    }
}