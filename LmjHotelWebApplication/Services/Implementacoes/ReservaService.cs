using LmjHotelWebApplication.Data;
using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Services.Contratos;
using System;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Implementacoes
{
    // Classe ReservaService implementando o contrato estabelecido na interface IReservaService
    public class ReservaService : IReservaService
    {
        // Injentando a dependência de SqlServerDbContext que realiza operações de acesso ao banco 
        private readonly SqlServerDbContext _context;

        public ReservaService(SqlServerDbContext context)
        {
            _context = context;
        }

        /* As Tasks estão sendo usadas para realizarmos operações assíncronas de acesso a dados,
         isso melhora a perfomance da aplicação, para isso devemos colocar a palavra async antes
         da palavra Task e acrescentar a palavra await antes da operação a ser realizada com o banco */

        public async Task SalvarReserva(Reserva reserva)
        {
            _context.Add(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task SalvarPagamento(Pagamento pagamento)
        {
            _context.Add(pagamento);
            await _context.SaveChangesAsync();
        }

        public bool ValidarReserva(DateTime inicio, DateTime termino)
        {
            var dataAtual = DateTime.Now;

            if (inicio < dataAtual || termino < dataAtual)
            {
                return false;
            }

            if (termino <= inicio)
            {
                return false;
            }
            return true;
        }
    }
}
