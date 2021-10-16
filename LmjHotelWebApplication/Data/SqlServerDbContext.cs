using LmjHotelWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace LmjHotelWebApplication.Data
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
        }

        // Injetando as classes Models que serão persistidas no Banco de Dados
        public DbSet<Hospede> Hospede { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Quarto> Quarto { get; set; }
        public DbSet<Pagamento> PagamentoComCartao { get; set; }
    }
}
