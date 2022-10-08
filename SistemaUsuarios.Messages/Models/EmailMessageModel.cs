using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Messages.Models
{
    /// <summary>
    /// Modelo de dados para envio de emails
    /// </summary>
    public class EmailMessageModel
    {
        /// <summary>
        /// Email do destinatário da mensagem
        /// </summary>
        public string MailTo { get; set; }

        /// <summary>
        /// Assunto da mensagem
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Conteudo / corpo da mensagem
        /// </summary>
        public string Body { get; set; }
    }
}
