namespace SistemaUsuarios.Api.Models
{
    /// <summary>
    /// Modelo de dados para consulta de histórico de usuário
    /// </summary>
    public class HistoricoModel
    {
        public Guid IdHistorico { get; set; }
        public DateTime DataHora { get; set; }
        public string Operacao { get; set; }
    }
}