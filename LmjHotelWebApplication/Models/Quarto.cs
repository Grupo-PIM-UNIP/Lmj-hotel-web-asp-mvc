using LmjHotelWebApplication.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmjHotelWebApplication.Models
{
    [Table("Tb_Quarto")]
    public class Quarto
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Quarto")]
        public string Numero { get; set; }
        public StatusQuarto EstadoDoQuarto { get; set; }
        public ICollection<Reserva> Reservas { get; } = new List<Reserva>();
    }
}
