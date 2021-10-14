using LmjHotelWebApplication.Models;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Contratos
{
    public interface IHospedeService
    {
        Task<Hospede> BuscaPorId(long id);
        Task Cadastrar(Hospede hospede);
    }
}
