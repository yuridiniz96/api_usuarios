using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaUsuarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade Historico
    /// </summary>
    public class HistoricoMap : IEntityTypeConfiguration<Historico>
    {
        //método para realizar o mapeamento da entidade
        public void Configure(EntityTypeBuilder<Historico> builder)
        {
            //nome da tabela
            builder.ToTable("HISTORICO");

            //chave primária
            builder.HasKey(h => h.IdHistorico);

            //mapeamento dos campos
            builder.Property(h => h.IdHistorico)
                .HasColumnName("IDHISTORICO")
                .IsRequired();

            builder.Property(h => h.IdUsuario)
                .HasColumnName("IDUSUARIO")
                .IsRequired();

            builder.Property(h => h.Operacao)
                .HasColumnName("OPERACAO")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(h => h.DataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            builder.Property(h => h.Detalhes)
               .HasColumnName("DETALHES")
               .HasMaxLength(500)
               .IsRequired();

            //mapeamento do campo chave estrangeira
            //cardinalidade de 1 para muitos
            builder.HasOne(h => h.Usuario) //Historico TEM 1 Usuário
                .WithMany(u => u.Historicos) //Usuário TEM MUITOS Históricos
                .HasForeignKey(h => h.IdUsuario); //chave estrangeira
        }
    }
}
