using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SistemaUsuarios.Data.Helpers
{
    /// <summary>
    /// Classe para criptografia MD5
    /// </summary>
    public class MD5Helper
    {
        //método para receber um valor e retorna-lo criptografado
        public static string Encrypt(string value)
        {
            //instanciando a classe para criptografia MD5
            var md5 = new MD5CryptoServiceProvider();

            //criptografando o valor recebido no método
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            //converter o conteudo do hash de bytes para string
            var saida = string.Empty;
            foreach (var item in hash)
            {
                saida += item.ToString("X2"); //X2 -> hexadecimal
            }

            return saida;
        }
    }
}