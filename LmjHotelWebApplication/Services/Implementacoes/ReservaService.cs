using LmjHotelWebApplication.Data;
using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Services.Contratos;
using LmjHotelWebApplication.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Implementacoes
{
    public class ReservaService : IReservaService
    {
        private readonly SqlServerDbContext _context;

        public ReservaService(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<Reserva> BuscarPorId(long id)
        {
            return await _context.Reserva.FirstOrDefaultAsync(reserva => reserva.Id.Equals(id));
        }

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
