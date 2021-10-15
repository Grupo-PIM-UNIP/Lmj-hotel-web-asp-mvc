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
        public string Pavimento { get; set; }
        public string Numero { get; set; }
        public Categoria Categoria { get; set; }
        public StatusQuarto Estado { get; set; }
        public ICollection<Reserva> Reservas { get; } = new List<Reserva>();
    }
}
