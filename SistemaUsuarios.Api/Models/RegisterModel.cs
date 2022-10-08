using System.ComponentModel.DataAnnotations;

namespace SistemaUsuarios.Api.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Por favor, informe o nome do usuário.")]
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        public string Email { get; set; }

        [StrongPassword(ErrorMessage = "Informe de 8 a 20 caracteres. Pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caracter especial(! @ # $ % &).")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Por favor, confirme a senha do usuário.")]
        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        public string SenhaConfirmacao { get; set; }

        public class StrongPassword : ValidationAttribute
        {
            //método para criar a regra de validação
            public override bool IsValid(object? value)
            {
                if (value != null)
                {
                    var senha = value.ToString();
                    return senha.Length >= 8 //minimo de 8 caracteres
                        && senha.Length <= 20 //maximo de 20 caracteres
                        && senha.Any(char.IsUpper)//pelo menos 1 caracter caixa alta
                        && senha.Any(char.IsLower)//pelo menos 2 caracter caixa baixa
                        && senha.Any(char.IsDigit)//pelo menos 1 digito numerico
                        && ( //pelo menos 1 dos caracteres especiais abaixo:
                            senha.Contains("@") || senha.Contains("#") || senha.Contains("$") ||
                            senha.Contains("%") || senha.Contains("&") || senha.Contains("!")
                           );
                }
                return false;
            }
        }
    }
}
