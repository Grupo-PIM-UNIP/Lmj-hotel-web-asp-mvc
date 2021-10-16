using LmjHotelWebApplication.Models;
using System;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Contratos
{
    public interface IReservaService
    {
        Task<Reserva> BuscarPorId(long id);
        Task SalvarReserva(Reserva reserva);
        Task SalvarPagamento(Pagamento pagamento);
        bool ValidarReserva(DateTime inicio, DateTime termino);
    }
}
