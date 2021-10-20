using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LmjHotelWebApplication.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_Hospede",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CartaoCredito = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Hospede", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Quarto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoDoQuarto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Quarto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Reserva",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecoPorDiaria = table.Column<double>(type: "float", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Tb_Pagamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instante = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    QtdParcelas = table.Column<int>(type: "int", nullable: false),
                    ReservaId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Pagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_Pagamento_Tb_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Tb_Reserva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Pagamento_ReservaId",
                table: "Tb_Pagamento",
                column: "ReservaId",
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
                name: "Tb_Pagamento");

            migrationBuilder.DropTable(
                name: "Tb_Reserva");

            migrationBuilder.DropTable(
                name: "Tb_Hospede");

            migrationBuilder.DropTable(
                name: "Tb_Quarto");
        }
    }
}
