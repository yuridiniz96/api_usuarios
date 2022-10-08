using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Data.Contexts;
using SistemaUsuarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Data.Repositories
{
    public class UsuarioRepository
    {
        //Método para inserir um usuário no banco de dados
        public void Create(Usuario usuario)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                //gravando um usuário no banco de dados
                sqlServerContext.Usuario.Add(usuario);
                sqlServerContext.SaveChanges();
            }
        }

        //método para atualizar um usuário no banco de dados
        public void Update(Usuario usuario)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                //atualizando o usuário do banco de dados
                sqlServerContext.Entry(usuario).State = EntityState.Modified;
                sqlServerContext.SaveChanges();
            }
        }

        //método para excluir um usuário no banco de dados
        public void Delete(Usuario usuario)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                //excluindo o usuário do banco de dados
                sqlServerContext.Remove(usuario);
                sqlServerContext.SaveChanges();
            }
        }

        //método para consultar 1 usuário através do email
        public Usuario GetByEmail(string email)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                //consultando 1 usuário pelo email
                return sqlServerContext.Usuario.FirstOrDefault(u => u.Email.Equals(email));
            }
        }

        //método para consultar 1 usuário através do email e da senha
        public Usuario GetByEmailAndSenha(string email, string senha)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                //consultando 1 usuário pelo email e senha
                return sqlServerContext.Usuario.FirstOrDefault(u => u.Email.Equals(email) && u.Senha.Equals(senha));
            }
        }
    }
}

