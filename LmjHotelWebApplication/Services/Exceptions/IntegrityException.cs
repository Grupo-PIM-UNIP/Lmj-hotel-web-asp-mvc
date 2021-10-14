using System;

namespace LmjHotelWebApplication.Services.Exceptions
{
    // Exceção personalizada para violação de integridade de dados entre os objetos
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message)
        {
        }
    }
}
