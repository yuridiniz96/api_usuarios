using static SistemaUsuarios.Api.Models.RegisterModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaUsuarios.Api.Models
{
    public class PasswordResetModel
    {
        [Required(ErrorMessage = "Por favor, informe sua senha atual.")]
        public string SenhaAtual { get; set; }


        [StrongPassword(ErrorMessage = "Informe de 8 a 20 caracteres. Pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caracter especial(! @ # $ % &).")]
        [Required(ErrorMessage = "Por favor, informe a nova senha de acesso.")]
        public string SenhaNova { get; set; }


        [Required(ErrorMessage = "Por favor, confirme a nova senha.")]
        [Compare("SenhaNova", ErrorMessage = "Senhas não conferem.")]
        public string SenhaNovaConfirmacao { get; set; }
    }
}
