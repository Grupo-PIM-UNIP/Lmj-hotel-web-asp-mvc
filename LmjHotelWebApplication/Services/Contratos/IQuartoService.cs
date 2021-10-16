using LmjHotelWebApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Contratos
{
    public interface IQuartoService
    {
        Task<ICollection<Quarto>> ListarQuartosDisponiveis();
        Task<Quarto> BuscarPorId(long id);
        Task Alugar(long id);
    }
}
