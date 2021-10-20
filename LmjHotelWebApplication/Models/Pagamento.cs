using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmjHotelWebApplication.Models
{
    [Table("Tb_Pagamento")]
    public class Pagamento
    {
        [Key]
        public long Id { get; set; }
        public DateTime Instante { get; set; }

        [Required(ErrorMessage = "O cálculo do valor total é obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Range(1, 12, ErrorMessage = "Número mínimo de parcelas é {1} e o máximo é {2}")]
        [DataType(DataType.CreditCard)]
        public int QtdParcelas { get; set; }

        public long ReservaId { get; set; }
        public Reserva Reserva { get; set; }
    }
}
