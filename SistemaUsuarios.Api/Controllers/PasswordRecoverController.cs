using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaUsuarios.Api.Models;
using SistemaUsuarios.Data.Entities;
using SistemaUsuarios.Data.Helpers;
using SistemaUsuarios.Data.Repositories;
using SistemaUsuarios.Messages.Models;
using SistemaUsuarios.Messages.Services;
namespace SistemaUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordRecoverController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(PasswordRecoverModel model)
        {
            try
            {
                var usuarioRepository = new UsuarioRepository();
                var historicoRepository = new HistoricoRepository();
                //buscar o usuário no banco de dados através do email
                var usuario = usuarioRepository.GetByEmail(model.Email);
                //verificar se o usuário foi encontrado
                if (usuario != null)
                {
                    //gerando uma nova senha
                    var faker = new Faker();

                    var novaSenha = $"@{faker.Internet.Password(10)}";
                    //enviando a senha por email

                    var emailMessageModel = new EmailMessageModel();
                    emailMessageModel.MailTo = usuario.Email;
                    emailMessageModel.Subject = "Recuperação de senha de acesso - Sistema Usuários";


                    emailMessageModel.Body = @$"
                        <div>
                            Olá, {usuario.Nome} <br/><br/> 
                            Utilise a senha: <strong>{novaSenha}</strong> para acessar sua conta. <br/><br/> 
                            Att, <br/>Sistema Usuários.
                        </div>
                    ";

                    var emailMessageService = new EmailMessageService();
                    emailMessageService.Send(emailMessageModel);

                    //atualizando a senha do usuário no banco de dados
                    usuario.Senha = MD5Helper.Encrypt(novaSenha);
                    usuarioRepository.Update(usuario);

                    //gravando um histórico desta operação
                    var historico = new Historico();
                    historico.IdHistorico = Guid.NewGuid();
                    historico.IdUsuario = usuario.IdUsuario;
                    historico.DataHora = DateTime.Now;
                    historico.Operacao = "Recuperação de Senha";

                    historico.Detalhes

                    = JsonConvert.SerializeObject(usuario);
                    historicoRepository.Create(historico);
                    //HTTP 200 (OK)

                    return StatusCode(200, new
                    {
                        mensagem = "Nova senha gerada com sucesso, por favor verifique seu email."
                    });

                }
                else
                {
                    //HTTP 400 (BAD REQUEST)
                    return StatusCode(400,

                    new
                    {
                        mensagem = "Email inválido. Usuário não encontrado."
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