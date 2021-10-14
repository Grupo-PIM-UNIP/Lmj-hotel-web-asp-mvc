using System;

namespace LmjHotelWebApplication.Services.Exceptions
{
    // Exceção personalizada para operações de Update no banco de dados
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string message) : base(message)
        {
        }
    }
}
