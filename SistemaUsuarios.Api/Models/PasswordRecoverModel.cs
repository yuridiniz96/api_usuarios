using System.ComponentModel.DataAnnotations;

namespace SistemaUsuarios.Api.Models
{
    public class PasswordRecoverModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email para recuperação da senha.")]
        public string Email { get; set; }
    }
}
