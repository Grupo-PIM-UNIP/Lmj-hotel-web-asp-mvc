using LmjHotelWebApplication.Data;
using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Models.Enums;
using LmjHotelWebApplication.Services.Contratos;
using LmjHotelWebApplication.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Services.Implementacoes
{
    // Classe QuartoService implementando o contrato estabelecido na interface IQuartoService
    public class QuartoService : IQuartoService
    {
        // Injentando a dependência de SqlServerDbContext que realiza operações de acesso ao banco 
        private readonly SqlServerDbContext _context;

        public QuartoService(SqlServerDbContext context)
        {
            _context = context;
        }

        /* As Tasks estão sendo usadas para realizarmos operações assíncronas de acesso a dados,
         isso melhora a perfomance da aplicação, para isso devemos colocar a palavra async antes
         da palavra Task e acrescentar a palavra await antes da operação a ser realizada com o banco */

        public async Task<Quarto> BuscarPorId(long id)
        {
            return await _context.Quarto.FirstOrDefaultAsync(quarto => quarto.Id.Equals(id));
        }

        public async Task<ICollection<Quarto>> ListarQuartosDisponiveis()
        {
            var estadoQuarto = StatusQuarto.Disponivel;

            var quartos =
                from quarto in _context.Quarto
                where quarto.Estado == estadoQuarto
                select quarto;

            return await quartos
                .OrderBy(quarto => quarto.Numero)
                .ToListAsync();
        }

        public async Task Alugar(long id)
        {
            var quarto = await BuscarPorId(id);
            if (quarto == null)
            {
                throw new NotFoundException("Quarto não encontrado no sistema");
            }

            try
            {
                quarto.Estado = StatusQuarto.Ocupado;
                _context.Quarto.Update(quarto);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
