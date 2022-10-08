using System.ComponentModel.DataAnnotations;

namespace SistemaUsuarios.Api.Models
{
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email de acesso.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha de acesso.")]
        public string Senha { get; set; }
    }
}
