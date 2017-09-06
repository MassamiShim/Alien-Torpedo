using System.ComponentModel.DataAnnotations;

namespace AlienTorpedoSite.ViewModels.Home
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [StringLength(80, ErrorMessage = "O [{0}] deve conter no máximo 80 caracteres!")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        public string Nm_email { get; set; }
        
        [Display(Name = "Senha")]
        [StringLength(20, ErrorMessage = "A campo [{0}] deve conter no máximo 20 caractéres!")]
        [Required(ErrorMessage = "O campo [{0}] é obrigatório!")]
        public string Nm_senha { get; set; }
    }
}