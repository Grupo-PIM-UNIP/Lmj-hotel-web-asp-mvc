using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmjHotelWebApplication.Models
{
    [Table("Tb_Reserva")]
    public class Reserva
    {
        [Key]
        public long Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public double Diaria { get; set; }
        public long HospedeId { get; set; }
        public Hospede Hospede { get; set; }
        public long QuartoId { get; set; }
        public Quarto Quarto { get; set; }
    }
}
