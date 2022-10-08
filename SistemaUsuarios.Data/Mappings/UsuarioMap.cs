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
    /// Classe para mapeamento da entidade usuario
    /// </summary>
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        //método para realizar o mapeamento da entidade
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //nome da tabela
            builder.ToTable("USUARIO");

            //chave primária
            builder.HasKey(u => u.IdUsuario);

            //mapeamento dos campos da tabela
            builder.Property(u => u.IdUsuario)
                .HasColumnName("IDUSUARIO")
                .IsRequired();

            builder.Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100)
                .IsRequired();

            //definindo o campo email como UNIQUE
            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.DataCriacao)
                .HasColumnName("DATACRIACAO")
                .IsRequired();

            builder.Property(u => u.DataUltimaAlteracao)
                .HasColumnName("DATAULTIMAALTERACAO")
                .IsRequired();
        }
    }
}

