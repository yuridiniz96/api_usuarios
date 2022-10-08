using SistemaUsuarios.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Messages.Services
{
    /// <summary>
    /// Classe para implementar o serviço de envio de email
    /// </summary>
    public class EmailMessageService
    {
        #region Parâmetros para envio de email

        private const string _conta = "cotinaoresponda@outlook.com";
        private const string _senha = "@Admin123456";
        private const string _smtp = "smtp-mail.outlook.com";
        private const int _port = 587;

        #endregion

        /// <summary>
        /// Método para realizar o envio de emails
        /// </summary>
        public void Send(EmailMessageModel model)
        {
            //criando o conteudo do email
            var mailMessage = new MailMessage(_conta, model.MailTo);
            mailMessage.Subject = model.Subject;
            mailMessage.Body = model.Body;
            mailMessage.IsBodyHtml = true;

            //enviando o email
            var smtpClient = new SmtpClient(_smtp, _port);
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}
