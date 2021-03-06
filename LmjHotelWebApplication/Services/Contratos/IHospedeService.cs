using LmjHotelWebApplication.Models;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Contratos
{
    public interface IHospedeService
    {
        Task<Hospede> BuscaPorId(long id);
        Task<Hospede> BuscaPorEmail(string email);
        Task Cadastrar(Hospede hospede);
        Task AtualizarCadastro(Hospede hospede);
        Task<bool> ValidarAcesso(long id, string email, string senha);
        Task RedefinirSenha(Hospede hospede, string senha);
    }
}
