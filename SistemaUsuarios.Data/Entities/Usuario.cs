using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Data.Entities
{
    /// <summary>
    /// Classe de entidade
    /// </summary>
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

        //Relacionamento com Historico
        //Usuario TEM MUITOS Historicos
        public List<Historico> Historicos { get; set; }
    }
}

