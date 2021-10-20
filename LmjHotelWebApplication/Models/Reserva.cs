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
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataFim { get; set; }

        public double PrecoPorDiaria { get; set; } = 150.00;
        public long HospedeId { get; set; }
        public Hospede Hospede { get; set; }

        [Display(Name = "Quarto")]
        public long QuartoId { get; set; }
        public Quarto Quarto { get; set; }
        public Pagamento Pagamento { get; set; }

        public Reserva()
        {
        }

        public double CalcularValorTotalDaHospedagem()
        {
            return PrecoPorDiaria * CalcularDiasDeHospedagem();
        }

        private int CalcularDiasDeHospedagem()
        {
            TimeSpan duracaoEmDias = DataFim.Subtract(DataInicio);
            return (int) duracaoEmDias.TotalDays;
        }
    }
}
