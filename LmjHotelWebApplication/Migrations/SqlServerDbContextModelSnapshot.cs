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
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

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

                    b.Property<long>("ReservaId")
                        .HasColumnType("bigint");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ReservaId")
                        .IsUnique();

                    b.ToTable("Tb_Pagamento");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Quarto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("EstadoDoQuarto")
                        .HasColumnType("int");

                    b.Property<string>("Identificacao")
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

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<long>("HospedeId")
                        .HasColumnType("bigint");

                    b.Property<double>("PrecoPorDiaria")
                        .HasColumnType("float");

                    b.Property<long>("QuartoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("HospedeId");

                    b.HasIndex("QuartoId");

                    b.ToTable("Tb_Reserva");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Pagamento", b =>
                {
                    b.HasOne("LmjHotelWebApplication.Models.Reserva", "Reserva")
                        .WithOne("Pagamento")
                        .HasForeignKey("LmjHotelWebApplication.Models.Pagamento", "ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Reserva", b =>
                {
                    b.HasOne("LmjHotelWebApplication.Models.Hospede", "Hospede")
                        .WithMany("Reservas")
                        .HasForeignKey("HospedeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LmjHotelWebApplication.Models.Quarto", "Quarto")
                        .WithMany("Reservas")
                        .HasForeignKey("QuartoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospede");

                    b.Navigation("Quarto");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Hospede", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Quarto", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("LmjHotelWebApplication.Models.Reserva", b =>
                {
                    b.Navigation("Pagamento");
                });
#pragma warning restore 612, 618
        }
    }
}
