using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaUsuarios.Api.Models;
using SistemaUsuarios.Data.Repositories;
namespace SistemaUsuarios.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var usuarioRepository = new UsuarioRepository();
                var historicoRepository = new HistoricoRepository();
                //capturar o email do usuário autenticado
                var email = User.Identity.Name;
                //consultar o usuario no banco de dados
                var usuario = usuarioRepository.GetByEmail(email);
                //consultar o historico deste usuário no banco de dados
                var historicos = historicoRepository.GetByUsuario
                (usuario.IdUsuario);
                //criando uma lista da classe model
                var lista = new List<HistoricoModel>();
                foreach (var item in historicos)
                {
                    var model = new HistoricoModel();
                    model.IdHistorico = item.IdHistorico;
                    model.DataHora = item.DataHora;
                    model.Operacao = item.Operacao;

                    lista.Add(model);
                }
                return Ok(lista); //retornando os dados da consulta da API
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}
