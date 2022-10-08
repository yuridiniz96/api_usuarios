using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaUsuarios.Api.Models;
using SistemaUsuarios.Data.Entities;
using SistemaUsuarios.Data.Helpers;
using SistemaUsuarios.Data.Repositories;

namespace SistemaUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(RegisterModel model)
        {
            try
            {
                var usuarioRepository = new UsuarioRepository();
                var historicoRepository = new HistoricoRepository();

                #region Verificar se o email informado já existe no banco de dados

                if (usuarioRepository.GetByEmail(model.Email) != null)
                {
                    //HTTP 400 (BAD REQUEST -> CLIENT ERROR)
                    return StatusCode(400, new { mensagem = "O email informado já está cadastrado, tente outro." });
                }

                #endregion

                #region Cadastrando o usuário

                var usuario = new Usuario();

                usuario.IdUsuario = Guid.NewGuid();
                usuario.Nome = model.Nome;
                usuario.Email = model.Email;
                usuario.Senha = MD5Helper.Encrypt(model.Senha);
                usuario.DataCriacao = DateTime.Now;
                usuario.DataUltimaAlteracao = DateTime.Now;

                usuarioRepository.Create(usuario);

                #endregion

                #region Cadastrando o histórico

                var historico = new Historico();

                historico.IdHistorico = Guid.NewGuid();
                historico.IdUsuario = usuario.IdUsuario;
                historico.DataHora = DateTime.Now;
                historico.Operacao = "Cadastro de Usuário";
                historico.Detalhes = JsonConvert.SerializeObject(usuario);

                historicoRepository.Create(historico);

                #endregion

                //HTTP 201 (CREATED)
                return StatusCode(201, new { mensagem = $"Usuário {usuario.Nome}, cadastrado com sucesso." });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}