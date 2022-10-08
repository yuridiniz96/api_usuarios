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
    public class HistoricoRepository
    {
        //método para inserir um historico no banco de dados
        public void Create(Historico historico)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                sqlServerContext.Historico.Add(historico);
                sqlServerContext.SaveChanges();
            }
        }

        //método para atualizar um historico no banco de dados
        public void Update(Historico historico)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                sqlServerContext.Entry(historico).State = EntityState.Modified;
                sqlServerContext.SaveChanges();
            }
        }

        //método para excluir um historico no banco de dados
        public void Delete(Historico historico)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                sqlServerContext.Historico.Remove(historico);
                sqlServerContext.SaveChanges();
            }
        }

        //método para consultar todos os históricos
        public List<Historico> GetAll()
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                return sqlServerContext.Historico
                    .OrderByDescending(h => h.DataHora)
                    .ToList();
            }
        }

        //método para consultar todos os históricos dentro de um período de datas
        public List<Historico> GetByDatas(DateTime dataMin, DateTime dataMax)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                return sqlServerContext.Historico
                    .Where(h => h.DataHora >= dataMin && h.DataHora <= dataMax)
                    .OrderByDescending(h => h.DataHora)
                    .ToList();
            }
        }

        //método para consultar todos os históricos de um determinado usuário
        public List<Historico> GetByUsuario(Guid idUsuario)
        {
            //conectando no banco de dados
            using (var sqlServerContext = new SqlServerContext())
            {
                return sqlServerContext.Historico
                    .Where(h => h.IdUsuario == idUsuario)
                    .OrderByDescending(h => h.DataHora)
                    .ToList();
            }
        }
    }
}
