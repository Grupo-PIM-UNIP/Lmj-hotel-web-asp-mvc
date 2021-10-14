using LmjHotelWebApplication.Models;
using System;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Contratos
{
    public interface IHospedeService
    {
        Task<Hospede> BuscaPorId(long id);
        Task<Hospede> BuscaPorEmail(string email);
        Task Cadastrar(Hospede hospede);
        Task<Boolean> ValidarAcesso(long id, string email, string senha);
        Task RedefinirSenha(Hospede hospede, string senha);
    }
}
