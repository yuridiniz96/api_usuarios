using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaUsuarios.Api.Models;
using SistemaUsuarios.Data.Entities;
using SistemaUsuarios.Data.Helpers;
using SistemaUsuarios.Data.Repositories;
namespace SistemaUsuarios.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        [HttpPut]
        public IActionResult Put(PasswordResetModel model)
        {
            try
            {
                var usuarioRepository = new UsuarioRepository();
                var historicoRepository = new HistoricoRepository();
                //capturar o email do usuário
                //contido no TOKEN de autenticação
                var email = User.Identity.Name;
                //consultar o usuário no banco de dados
                var usuario = usuarioRepository.GetByEmailAndSenha
                (email, MD5Helper.Encrypt(model.SenhaAtual));

                //verificando se o usuário foi encontrado
                if (usuario != null)
                {
                    //atualizando a senha do usuário

                    usuario.Senha = MD5Helper.Encrypt(model.SenhaNova);
                    usuarioRepository.Update(usuario);
                    //gravando o histórico desta operação
                    var historico = new Historico();
                    historico.IdHistorico = Guid.NewGuid();
                    historico.IdUsuario = usuario.IdUsuario;
                    historico.DataHora = DateTime.Now;
                    historico.Operacao = "Atualização de Senha";
                    historico.Detalhes = JsonConvert.SerializeObject

                    (usuario);
                    historicoRepository.Create(historico);
                    //HTTP 200 (OK)
                    return StatusCode(200, new
                    {
                        mensagem = "Senha de acesso atualizada com sucesso."
                    });

                }
                else
                {
                    //HTTP 400 (BAD REQUEST)

                    return StatusCode(400, new
                    {
                        mensagem = "Senha atual inválida, por favor verifique."
                    });

                }
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}