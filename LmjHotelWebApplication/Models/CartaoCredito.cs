using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmjHotelWebApplication.Models
{
    [Table("Tb_Cartao_Credito")]
    public class CartaoCredito
    {

        [Key]
        public long Id { get; set; }
        public int Numero { get; set; }
        public DateTime Validade { get; set; }
        public long PagamentoId { get; set; }
        public Pagamento Pagamento { get; set; }
    }
}
