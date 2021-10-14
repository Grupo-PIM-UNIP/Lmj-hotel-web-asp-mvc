using System;

namespace LmjHotelWebApplication.Services.Exceptions
{
    // Exceção personalizada para objetos não encontrados
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
