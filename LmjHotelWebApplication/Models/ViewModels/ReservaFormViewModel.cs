using System.Collections.Generic;

namespace LmjHotelWebApplication.Models.ViewModels
{
    public class ReservaFormViewModel
    {
        public long Id { get; set; }
        public Reserva Reserva { get; set; }
        public ICollection<Quarto> Quartos { get; set; }
        public Pagamento Pagamento { get; set; }
    }
}
