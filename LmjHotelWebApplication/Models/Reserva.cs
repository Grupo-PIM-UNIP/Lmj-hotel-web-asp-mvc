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

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Inicio { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fim { get; set; }

        public long HospedeId { get; set; }
        public Hospede Hospede { get; set; }

        [Display(Name = "Quarto")]
        public long QuartoId { get; set; }
        public Quarto Quarto { get; set; }

        public long PagamentoId { get; set; }
        public Pagamento Pagamento { get; set; }
    }
}
