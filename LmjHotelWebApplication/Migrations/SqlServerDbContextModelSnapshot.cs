﻿// <auto-generated />
using System;
using LmjHotelWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LmjHotelWebApplication.Migrations
{
    [DbContext(typeof(SqlServerDbContext))]
    partial class SqlServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("LmjHotelWebApplication.Models.Hospede", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("CartaoCredito")
                        .IsRequired()
                        .HasMaxLength(19)
                        .HasColumnType("nvarchar(19)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.ToTable("Tb_Hospede");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Pagamento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Instante")
                        .HasColumnType("datetime2");

                    b.Property<int>("QtdParcelas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tb_Pagamento");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Quarto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tb_Quarto");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Reserva", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Fim")
                        .HasColumnType("datetime2");

                    b.Property<long>("HospedeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2");

                    b.Property<long>("PagamentoId")
                        .HasColumnType("bigint");

                    b.Property<long>("QuartoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("HospedeId");

                    b.HasIndex("PagamentoId")
                        .IsUnique();

                    b.HasIndex("QuartoId");

                    b.ToTable("Tb_Reserva");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Reserva", b =>
                {
                    b.HasOne("LmjHotelWebApplication.Models.Hospede", "Hospede")
                        .WithMany("Reservas")
                        .HasForeignKey("HospedeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LmjHotelWebApplication.Models.Pagamento", "Pagamento")
                        .WithOne("Reserva")
                        .HasForeignKey("LmjHotelWebApplication.Models.Reserva", "PagamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LmjHotelWebApplication.Models.Quarto", "Quarto")
                        .WithMany("Reservas")
                        .HasForeignKey("QuartoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospede");

                    b.Navigation("Pagamento");

                    b.Navigation("Quarto");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Hospede", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Pagamento", b =>
                {
                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Quarto", b =>
                {
                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
