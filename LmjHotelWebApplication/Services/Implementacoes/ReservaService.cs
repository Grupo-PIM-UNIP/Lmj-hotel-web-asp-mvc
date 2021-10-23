using LmjHotelWebApplication.Data;
using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Services.Contratos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LmjHotelWebApplication.Services.Exceptions;

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

        public async Task<ICollection<Reserva>> ListarMinhasReservas(long hospedeId)
        {
            var minhasReservas =
                from reserva in _context.Reserva
                where reserva.HospedeId == hospedeId
                select reserva;

            return await minhasReservas
               .Include(obj => obj.Quarto)
               .Include(obj => obj.Pagamento)
               .OrderByDescending(reserva => reserva.DataInicio)
               .ToListAsync();
        }

        public async Task SalvarReserva(Reserva reserva)
        {
            try
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task SalvarPagamento(Pagamento pagamento)
        {
            try
            {
                _context.Add(pagamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
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
