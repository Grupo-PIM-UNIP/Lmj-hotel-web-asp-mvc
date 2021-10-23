using LmjHotelWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Contratos
{
    public interface IReservaService
    {
        Task SalvarReserva(Reserva reserva);
        Task SalvarPagamento(Pagamento pagamento);
        bool ValidarReserva(DateTime inicio, DateTime termino);
        Task<ICollection<Reserva>> ListarMinhasReservas(long hospedeId);
    }
}
