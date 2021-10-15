﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LmjHotelWebApplication.Migrations
{
    public partial class OthersEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Tb_Hospede",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Tb_Pagamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instante = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QtdParcelas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Pagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Quarto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pavimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Quarto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Cartao_Credito",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PagamentoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Cartao_Credito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_Cartao_Credito_Tb_Pagamento_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Tb_Pagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Reserva",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diaria = table.Column<double>(type: "float", nullable: false),
                    HospedeId = table.Column<long>(type: "bigint", nullable: false),
                    QuartoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_Reserva_Tb_Hospede_HospedeId",
                        column: x => x.HospedeId,
                        principalTable: "Tb_Hospede",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_Reserva_Tb_Quarto_QuartoId",
                        column: x => x.QuartoId,
                        principalTable: "Tb_Quarto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Cartao_Credito_PagamentoId",
                table: "Tb_Cartao_Credito",
                column: "PagamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Reserva_HospedeId",
                table: "Tb_Reserva",
                column: "HospedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Reserva_QuartoId",
                table: "Tb_Reserva",
                column: "QuartoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_Cartao_Credito");

            migrationBuilder.DropTable(
                name: "Tb_Reserva");

            migrationBuilder.DropTable(
                name: "Tb_Pagamento");

            migrationBuilder.DropTable(
                name: "Tb_Quarto");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Tb_Hospede",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);
        }
    }
}
