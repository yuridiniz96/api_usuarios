using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace SistemaUsuarios.Api.Security
{
    public static class JwtSecurity
    {
        #region Atributos para parametrizar os tokens
        public static int ExpirationInHours = 6;
        public static string SecretKey = "c8b803e0-b90d-4281-a9fe-5fef2cfb92c6";

        #endregion
        //método para fazermos a geração do TOKEN
        public static string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            //conteudo do token
            var tokenDescription = new SecurityTokenDescriptor
            {
                //criando a identificação do usuario para o AspNet
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, email) //email do usuario
            }),
                //definindo a data de expiração do Token
                Expires = DateTime.Now.AddHours(ExpirationInHours),
                //criptografia do Token a chave
                //secreta (evitar falsificação)

                SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}