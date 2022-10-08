using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SistemaUsuarios.Api.Models;
using SistemaUsuarios.Api.Security;
using SistemaUsuarios.Data.Entities;
using SistemaUsuarios.Data.Helpers;
using SistemaUsuarios.Data.Repositories;
namespace SistemaUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(LoginModel model)
        {
            try
            {
                var usuarioRepository = new UsuarioRepository();
                var historicoRepository = new HistoricoRepository();
                #region Consultar o usuário no banco de dados através do email e da senha

                var usuario = usuarioRepository

                .GetByEmailAndSenha(model.Email,
                MD5Helper.Encrypt(model.Senha));

                if (usuario == null)
                {
                    //HTTP 400 (BAD REQUEST -> CLIENT ERROR)

                    return StatusCode(400,

                    new
                    {
                        mensagem = "Acesso negado. Usuário inválido." });

                    }
                    //gerar um token para o usuário autenticado
                    var token = JwtSecurity.GenerateToken(usuario.Email);
                    //registrar o histórico
                    var historico = new Historico();
                    historico.IdHistorico = Guid.NewGuid();
                    historico.IdUsuario = usuario.IdUsuario;
                    historico.DataHora = DateTime.Now;
                    historico.Operacao = "Autenticação de usuário";
                    historico.Detalhes = JsonConvert.SerializeObject(usuario);
                    historicoRepository.Create(historico);
                    //retornar o TOKEN
                    return StatusCode(200,

                    new
                    {
                        mensagem = "Autenticação relizada com sucesso", token });

                        #endregion
                    }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}