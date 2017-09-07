using System.ComponentModel.DataAnnotations;

namespace AlienTorpedoSite.ViewModels.Conta
{
    public class EntrarViewModel
    {
        
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail invalido!")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        [StringLength(80, ErrorMessage = "O [{0}] deve conter no máximo 80 caracteres!")]
        public string Nm_email { get; set; }
        
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        [StringLength(20, ErrorMessage = "A campo [{0}] deve conter no máximo 20 caractéres!")]
        public string Nm_senha { get; set; }
        
    }
}