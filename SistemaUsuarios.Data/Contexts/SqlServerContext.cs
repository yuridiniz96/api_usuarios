using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Data.Entities;
using SistemaUsuarios.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Data.Contexts
{
    /// <summary>
    /// Classe para acessar o banco de dados do SqlServer
    /// através do EntityFramework (classe de conexão com o BD)
    /// </summary>
    public class SqlServerContext : DbContext
    {
        //método para fazer com que o EntityFramework possa
        //conectar-se no banco de dados do projeto
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //definir a connectionstring do banco de dados
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSistemaUsuarios;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //método para incluir cada classe de mapeamento
        //feita no projeto com o EntityFramework
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adicionar cada classe de mapeamento do projeto
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new HistoricoMap());
        }

        //criar uma propriedade DbSet para cada classe de entidade
        //Este DbSet vai disponibilizar para cada entidade os métodos
        //de CRUD do banco de dados (INSERT, UPDATE, DELETE, SELECT etc)
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Historico> Historico { get; set; }
    }
}




