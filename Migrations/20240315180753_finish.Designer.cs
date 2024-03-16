﻿// <auto-generated />
using System;
using AlmoxarifadoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AlmoxarifadoAPI.Migrations
{
    [DbContext(typeof(AlmoxarifadoAPIContext))]
    [Migration("20240315180753_finish")]
    partial class finish
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AlmoxarifadoAPI.Models.Email", b =>
                {
                    b.Property<int?>("idEmail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idEmail");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("idEmail"), 1L, 1);

                    b.Property<string>("EmailUsuario")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("EmailUsuario");

                    b.HasKey("idEmail");

                    b.ToTable("Emails", (string)null);
                });

            modelBuilder.Entity("AlmoxarifadoAPI.Models.GestaoProduto", b =>
                {
                    b.Property<int>("IdProduto")
                        .HasColumnType("int")
                        .HasColumnName("idProduto");

                    b.Property<string>("Descricao")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("descricao");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("estado");

                    b.Property<int?>("EstoqueAtual")
                        .HasColumnType("int")
                        .HasColumnName("estoqueAtual");

                    b.Property<int?>("EstoqueMinimo")
                        .HasColumnType("int")
                        .HasColumnName("estoqueMinimo");

                    b.Property<decimal?>("Preco")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("preco");

                    b.HasKey("IdProduto")
                        .HasName("PK__GestaoPr__5EEDF7C3DE3C8957");

                    b.ToTable("GestaoProdutos");
                });

            modelBuilder.Entity("AlmoxarifadoAPI.Models.Logrobo", b =>
                {
                    b.Property<int>("IDlOg")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("iDlOG");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDlOg"), 1L, 1);

                    b.Property<string>("CodigoRobo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateLog")
                        .HasColumnType("datetime2");

                    b.Property<string>("Etapa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdProdutoAPI")
                        .HasColumnType("int")
                        .HasColumnName("IdProdutoAPI");

                    b.Property<string>("InformacaoLog")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioRobo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDlOg");

                    b.ToTable("LOGROBO", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
