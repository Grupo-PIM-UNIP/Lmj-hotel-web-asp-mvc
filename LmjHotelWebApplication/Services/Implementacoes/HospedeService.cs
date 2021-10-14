using LmjHotelWebApplication.Data;
using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Services.Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Implementacoes
{
    // Classe HospedeService implementando o contrato estabelecido na interface IHospedeService
    public class HospedeService : IHospedeService
    {
        // Injentando a dependência de SqlServerDbContext que realiza operações de acesso ao banco 
        private readonly SqlServerDbContext _context;

        public HospedeService(SqlServerDbContext context)
        {
            _context = context;
        }

        /* As Tasks estão sendo usadas para realizarmos operações assíncronas de acesso a dados,
          isso melhora a perfomance da aplicação, para isso devemos colocar a palavra async antes
          da palavra Task e acrescentar a palavra await antes da operação a ser realizada com o banco */

        public async Task<Hospede> BuscaPorId(long id)
        {
            return await _context.Hospede.FirstOrDefaultAsync(hospede => hospede.Id.Equals(id));
        }

        public async Task<Hospede> BuscaPorEmail(string email)
        {
            return await _context.Hospede.FirstOrDefaultAsync(hospede => hospede.Email.Equals(email));
        }

        public async Task Cadastrar(Hospede hospede)
        {
            string hashSenha = EncriptarSenhaSHA256(hospede.Senha);
            hospede.Senha = hashSenha;

            _context.Add(hospede);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidarAcesso(long id, string email, string senha)
        {
            var hospede = await BuscaPorId(id);
            string hashSenha = EncriptarSenhaSHA256(senha);

            if (hospede.Email.Equals(email) && hospede.Senha.Equals(hashSenha))
            {
                return true;
            }
            return false;
        }

        public async Task RedefinirSenha(Hospede hospede, string senha)
        {
            string hashSenha = EncriptarSenhaSHA256(senha);
            hospede.Senha = hashSenha;

            _context.Update(hospede);
            await _context.SaveChangesAsync();
        }


        // Método privado usado para encriptografar a senha do usuário antes de fazer a persistência no banco
        private string EncriptarSenhaSHA256(string senha)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

            var sb = new StringBuilder();
            for (int i=0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
